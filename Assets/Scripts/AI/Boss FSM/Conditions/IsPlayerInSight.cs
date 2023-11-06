using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.Conditions.BossConditions
{
    [CreateAssetMenu(fileName = "Is Player In Sight?", menuName = "ScriptableObject/FSM Conditions/Boss Conditions/Is Player In Sight?")]
    public class IsPlayerInSight : StateCondition
    {
        public override bool CompleteCondition(Enemy model)
        {
            var playerInstance = GameManager.Instance.PlayerInstance;
            return model.gameObject.GetComponent<BossAI>().BossLineOfSight.targetInSight;
        }
    }
}
