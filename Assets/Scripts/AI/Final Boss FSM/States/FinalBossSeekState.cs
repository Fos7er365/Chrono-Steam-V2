using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Seek State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Seek State", order = 0)]
    public class FinalBossSeekState : State
    {
        Dictionary<Enemy, FinalBossSeekData> seekData = new Dictionary<Enemy, FinalBossSeekData>();

        private class FinalBossSeekData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public FinalBossFSMStats fsmStats;
            public FinalBossEnemyCombat combatHandler;
            public FinalBossSeekData(Enemy model)
            {
                bossModel = model;
                bossAI = model.gameObject.GetComponent<BossAI>();
                fsmStats = bossAI.FsmConditionsStats as FinalBossFSMStats;
                combatHandler = bossModel.GetComponent<FinalBossEnemyCombat>();
            }

        }

        public override void EnterState(Enemy model)
        {
            if (!seekData.ContainsKey(model)) seekData.Add(model, new FinalBossSeekData(model));

        }

        public override void ExecuteState(Enemy model)
        {
            var dist = Vector3.Distance(seekData[model].bossModel.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            SeekBehaviour(seekData[model].bossAI);
            if (dist < seekData[model].bossModel.Stats.AttackRange)
            {
                CheckTransitionToAttackStateHandler(seekData[model].bossModel);
                CheckTransitionToSummonAttackStateHandler(seekData[model].bossModel);
                CheckTransitionToDesperateAttackStateHandler(seekData[model].bossModel);
            }
        }

        public override void ExitState(Enemy model)
        {
            seekData.Remove(model);
        }

        void SeekBehaviour(BossAI ai)
        {
            ai.BossSeekSB.move = true;
        }

        void CheckTransitionToAttackStateHandler(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife > seekData[model].combatHandler.RegularAttackThreshold)
                seekData[model].fsmStats.IsBelowAttackHealth = false;
        }

        void CheckTransitionToSummonAttackStateHandler(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife < seekData[model].combatHandler.RegularAttackThreshold)
                seekData[model].fsmStats.IsBelowAttackHealth = true;
        }

        void CheckTransitionToDesperateAttackStateHandler(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife < seekData[model].combatHandler.SummonAttackThreshold)
                seekData[model].fsmStats.IsBelowSummonAttackHealth = true;
        }

    }
}
