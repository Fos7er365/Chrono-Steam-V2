using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Block Attack State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Block Attack State", order = 0)]
    public class FinalBossBlockState : State
    {
        Dictionary<Enemy, FinalBossBlockData> blockData = new Dictionary<Enemy, FinalBossBlockData>();

        private class FinalBossBlockData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public FinalBossFSMStats fsmStats;
            public FinalBossEnemyCombat combatHandler;
            public float blockStateTimer;
            public FinalBossBlockData(Enemy model)
            {
                bossModel = model;
                bossAI = model.gameObject.GetComponent<BossAI>();
                fsmStats = bossAI.FsmConditionsStats as FinalBossFSMStats;
                combatHandler = bossModel.GetComponent<FinalBossEnemyCombat>();
                blockStateTimer = 0;
            }

        }

        public override void EnterState(Enemy model)
        {
            if (!blockData.ContainsKey(model)) blockData.Add(model, new FinalBossBlockData(model));
        }

        public override void ExecuteState(Enemy model)
        {
            var bossModel = blockData[model].bossModel;
            var dist = Vector3.Distance(bossModel.transform.position, GameManager.Instance.PlayerInstance.transform.position);

            blockData[model].combatHandler.BlockAttacks();

            CheckTransitionToSeekState(dist, bossModel.Stats.AttackRange,
                blockData[model].fsmStats.IsInAttackRange, false);
            CheckTransitionToAttackStateHandler(blockData[model].bossModel);
            CheckTransitionToSummonAttackStateHandler(blockData[model].bossModel);
            
        }

        public override void ExitState(Enemy model)
        {
            blockData.Remove(model);
        }
        void CheckTransitionToSeekState(float distance, float attackRange, bool isTransition, bool transitionvalue)
        {
            if (distance > attackRange) isTransition = transitionvalue;
        }
        void CheckTransitionToAttackStateHandler(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife > blockData[model].combatHandler.RegularAttackThreshold)
                blockData[model].fsmStats.IsBelowAttackHealth = false;
        }

        void CheckTransitionToSummonAttackStateHandler(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife > blockData[model].combatHandler.RegularAttackThreshold)
                blockData[model].fsmStats.IsBelowAttackHealth = true;
        }

    }
}
