using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Idle State", order = 0)]
    public class FinalBossIdleState : State
    {
        Dictionary<Enemy, FinalBossIdleData> idleData = new Dictionary<Enemy, FinalBossIdleData>();

        private class FinalBossIdleData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public FinalBossFSMStats fsmStats;
            public FinalBossIdleData(Enemy model)
            {
                bossModel = model;
                bossAI = model.gameObject.GetComponent<BossAI>();
                fsmStats = bossAI.FsmConditionsStats as FinalBossFSMStats;
            }

        }

        public override void EnterState(Enemy model)
        {
            if (!idleData.ContainsKey(model)) idleData.Add(model, new FinalBossIdleData(model));
            idleData[model].fsmStats.CanRunFSM = true;
        }

        public override void ExecuteState(Enemy model)
        {
        }

        public override void ExitState(Enemy model)
        {
            idleData.Remove(model);
        }
    }
}
