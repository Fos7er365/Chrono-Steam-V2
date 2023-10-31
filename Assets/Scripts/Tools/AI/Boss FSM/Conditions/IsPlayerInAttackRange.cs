using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.Conditions.BossConditions
{
    [CreateAssetMenu(fileName = "Is Player In Attack Range?", menuName = "ScriptableObject/FSM Conditions/Boss Conditions/Is Player In Attack Range?")]
    public class IsPlayerInAttackRange : StateCondition
    {
        public override bool CompleteCondition(Enemy model)
        {
            var playerInstance = GameManager.Instance.PlayerInstance;
            return Vector3.Distance(model.transform.position, playerInstance.transform.position) < model.Stats.AttackRange;
        }
    }
}
