using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Summon Attack State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Summon Attack State", order = 0)]
    public class FinalBossSummonAttackState : State
    {
        Dictionary<Enemy, FinalBossSummonAttackData> summonAttackData = new Dictionary<Enemy, FinalBossSummonAttackData>();

        private class FinalBossSummonAttackData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public FinalBossFSMStats fsmStats;
            public FinalBossEnemyCombat bossCombatHandler;
            public float attackTimer;
            public FinalBossSummonAttackData(Enemy model)
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
            if (!summonAttackData.ContainsKey(model)) summonAttackData.Add(model, new FinalBossSummonAttackData(model));
        }

        public override void ExecuteState(Enemy model)
        {
            var bossModel = summonAttackData[model].bossModel;
            var dist = Vector3.Distance(bossModel.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            summonAttackData[model].attackTimer += Time.deltaTime;

            if (summonAttackData[model].attackTimer < summonAttackData[model].bossCombatHandler.RegularAttackTimer)
            {
                summonAttackData[model].bossCombatHandler.SummonAttack();
                summonAttackData[model].fsmStats.IsBlocking = true;
                CheckTransitionToSeekState(dist, bossModel.Stats.AttackRange,
                    summonAttackData[model].fsmStats.IsInAttackRange, false);
                CheckTransitionToDesperateAttackStateHandler(summonAttackData[model].bossModel);
            }
            else
            {
                summonAttackData[model].fsmStats.IsBlocking = true;
                summonAttackData[model].attackTimer = 0;
            }
        }

        public override void ExitState(Enemy model)
        {
            summonAttackData.Remove(model);
        }
        void CheckTransitionToDesperateAttackStateHandler(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife < summonAttackData[model].bossCombatHandler.SummonAttackThreshold)
                summonAttackData[model].fsmStats.IsBelowSummonAttackHealth = true;
        }

        void CheckTransitionToSeekState(float distance, float attackRange, bool isTransition, bool transitionvalue)
        {
            if (distance > attackRange) isTransition = transitionvalue;
        }
    }
}
