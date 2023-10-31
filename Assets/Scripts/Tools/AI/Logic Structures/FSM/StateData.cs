using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION
{
    [CreateAssetMenu(fileName = "State Data", menuName = "ScriptableObject/FSM States/FSM State Data", order = 0)]
    public class StateData : ScriptableObject
    {
        [SerializeField] State state;
        [SerializeField] StateCondition[] stateConditions;
        [SerializeField] StateData[] exitStates;

        public State State { get => state; set => state = value; }
        public StateCondition[] StateConditions { get => stateConditions; set => stateConditions = value; }
        public StateData[] ExitStates { get => exitStates; set => exitStates = value; }
    }
}