using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.Conditions.BossConditions
{
    [CreateAssetMenu(fileName = "Is Health Below Summon Attack Threshold?", menuName = "ScriptableObject/FSM Conditions/Final Boss Conditions/Is Health Below Summon Attack Threshold?")]
    public class IsHealthBelowSummonAttackThreshold : StateCondition
    {
        public override bool CompleteCondition(Enemy model)
        {
            var conditions = model.gameObject.GetComponent<BossAI>().FsmConditionsStats as FinalBossFSMStats;
            var threshold = model.gameObject.GetComponent<FinalBossEnemyCombat>().SummonAttackThreshold;
            return conditions.IsBelowSummonAttackHealth;
        }
    }
}