using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Desperate Attack State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Desperate Attack State", order = 0)]
    public class FinalBossDesperateAttackState : State
    {
        Dictionary<Enemy, FinalBossDesperateAttackData> desperateAttackData = new Dictionary<Enemy, FinalBossDesperateAttackData>();

        private class FinalBossDesperateAttackData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public FinalBossFSMStats fsmStats;
            public FinalBossEnemyCombat bossCombatHandler;
            public float attackTimer;
            public FinalBossDesperateAttackData(Enemy model)
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
            if (!desperateAttackData.ContainsKey(model)) desperateAttackData.Add(model, new FinalBossDesperateAttackData(model));
        }

        public override void ExecuteState(Enemy model)
        {
            var bossModel = desperateAttackData[model].bossModel;
            var dist = Vector3.Distance(bossModel.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            desperateAttackData[model].attackTimer += Time.deltaTime;

            if (desperateAttackData[model].attackTimer < desperateAttackData[model].bossCombatHandler.RegularAttackTimer)
            {
                desperateAttackData[model].bossCombatHandler.Attack();
                CheckTransitionToSeekState(dist, bossModel.Stats.AttackRange,
                    desperateAttackData[model].fsmStats.IsInAttackRange, false);
            }
            else
            {
                desperateAttackData[model].attackTimer = 0;
            }

        }

        public override void ExitState(Enemy model)
        {
            desperateAttackData.Remove(model);
        }
        void CheckTransitionToSeekState(float distance, float attackRange, bool isTransition, bool transitionvalue)
        {
            if (distance > attackRange) isTransition = transitionvalue;
        }
    }
}
