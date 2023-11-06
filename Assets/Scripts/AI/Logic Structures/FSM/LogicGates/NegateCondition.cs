using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.LogicGates
{
    [CreateAssetMenu(fileName = "Negate Condition", menuName = "ScriptableObject/FSM Conditions/Logic/NEGATE Condition")]
    public class NegateCondition : StateCondition
    {
        [SerializeField] private StateCondition condition;
        public override bool CompleteCondition(Enemy model)
        {
            return !condition.CompleteCondition(model);
        }
    }
}