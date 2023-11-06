using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Regular Attack State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Regular Attack State", order = 0)]
    public class FinalBossRegularAttackState : State
    {
        Dictionary<Enemy, FinalBossRegularAttackData> regularAttackData = new Dictionary<Enemy, FinalBossRegularAttackData>();

        private class FinalBossRegularAttackData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public FinalBossFSMStats fsmStats;
            public FinalBossEnemyCombat bossCombatHandler;
            public float attackTimer;
            public FinalBossRegularAttackData(Enemy model)
            {
                bossModel = model;
                bossAI = model.gameObject.GetComponent<BossAI>();
                fsmStats = bossAI.FsmConditionsStats as FinalBossFSMStats;
                bossCombatHandler = bossModel.GetComponent<FinalBossEnemyCombat>();
                attackTimer = 0;
            }

        }

        public override void EnterState(Enemy model)
        {
            if (!regularAttackData.ContainsKey(model)) regularAttackData.Add(model, new FinalBossRegularAttackData(model));
        }

        public override void ExecuteState(Enemy model)
        {
            var bossModel = regularAttackData[model].bossModel;
            var dist = Vector3.Distance(bossModel.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            regularAttackData[model].attackTimer += Time.deltaTime;

            if (regularAttackData[model].attackTimer < regularAttackData[model].bossCombatHandler.RegularAttackTimer)
            {
                regularAttackData[model].bossCombatHandler.Attack();
                CheckTransitionToSummonAttackStateHandler(regularAttackData[model].bossModel);
                CheckTransitionToSeekState(dist, bossModel.Stats.AttackRange,
                    regularAttackData[model].fsmStats.IsInAttackRange, false);
            }
            else
            {
                regularAttackData[model].fsmStats.IsBlocking = true;
                regularAttackData[model].attackTimer = 0;
            }
        }

        public override void ExitState(Enemy model)
        {
            regularAttackData.Remove(model);
        }
        void CheckTransitionToSummonAttackStateHandler(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife < regularAttackData[model].bossCombatHandler.RegularAttackThreshold)
                regularAttackData[model].fsmStats.IsBelowAttackHealth = true;
        }

        void CheckTransitionToSeekState(float distance, float attackRange, bool isTransition, bool transitionvalue)
        {
            if (distance > attackRange) isTransition = transitionvalue;
        }
    }
}
