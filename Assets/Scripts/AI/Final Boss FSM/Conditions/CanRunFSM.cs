using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.Conditions.BossConditions
{
    [CreateAssetMenu(fileName = "Can Run FSM?", menuName = "ScriptableObject/FSM Conditions/Final Boss Conditions/Can Run FSM?")]
    public class CanRunFSM : StateCondition
    {
        public override bool CompleteCondition(Enemy model)
        {
            var conditions = model.gameObject.GetComponent<BossAI>().FsmConditionsStats as FinalBossFSMStats;
            return conditions.CanRunFSM;

        }
    }
}
