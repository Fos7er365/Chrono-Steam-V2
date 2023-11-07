using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.Conditions.BossConditions
{
    [CreateAssetMenu(fileName = "Is In Attack Range?", menuName = "ScriptableObject/FSM Conditions/Final Boss Conditions/Is In Attack Range?")]
    public class IsInAttackRange : StateCondition
    {
        public override bool CompleteCondition(Enemy model)
        {
            var conditions = model.gameObject.GetComponent<BossAI>().FsmConditionsStats as FinalBossFSMStats;
            return conditions.IsInAttackRange;

        }
    }
}