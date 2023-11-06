using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.Conditions.BossConditions
{
    [CreateAssetMenu(fileName = "Is Health Below Regular Attack Threshold?", menuName = "ScriptableObject/FSM Conditions/Final Boss Conditions/Is Health Below Regular Attack Threshold?")]
    public class IsHealthBelowRegularAttackThreshold : StateCondition
    {
        public override bool CompleteCondition(Enemy model)
        {
            var conditions = model.gameObject.GetComponent<BossAI>().FsmConditionsStats as FinalBossFSMStats;
            var threshold = model.gameObject.GetComponent<FinalBossEnemyCombat>().RegularAttackThreshold;
            return conditions.IsBelowAttackHealth = model.EnemyHealthController.CurrentLife < threshold;

        }
    }
}