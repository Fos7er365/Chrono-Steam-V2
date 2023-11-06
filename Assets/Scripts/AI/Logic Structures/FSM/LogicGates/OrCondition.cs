using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.LogicGates
{
    [CreateAssetMenu(fileName = "Or Condition", menuName = "ScriptableObject/FSM Conditions/Logic/OR Condition")]
    public class OrCondition : StateCondition
    {
        [SerializeField] private StateCondition conditionOne;
        [SerializeField] private StateCondition conditionTwo;
        public override bool CompleteCondition(Enemy model)
        {
            return conditionOne.CompleteCondition(model) || conditionTwo.CompleteCondition(model);
        }
    }
}