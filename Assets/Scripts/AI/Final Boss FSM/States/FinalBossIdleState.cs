using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Idle State", order = 0)]
    public class FinalBossIdleState : State
    {

        FinalBossFSMStats stats;
        FinalBossEnemyCombat combat;
        BossAI ai;

        public override void EnterState(Enemy model)
        {
            Debug.Log("Final Boss FSM IDLE state ENTER");
            ai = model.gameObject.GetComponent<BossAI>();
            stats = ai.FsmConditionsStats as FinalBossFSMStats;
            combat = model.gameObject.GetComponent<FinalBossEnemyCombat>();
        }

        public override void ExecuteState(Enemy model)
        {
            Debug.Log("Final Boss FSM IDLE state EXECUTE");
            var dist = Vector3.Distance(model.gameObject.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            if(dist > model.Stats.AttackRange)
            {
                stats.IsInAttackRange = false;
            }
        }

        public override void ExitState(Enemy model)
        {
            Debug.Log("Final Boss FSM IDLE state EXIT");
        }
    }
}
