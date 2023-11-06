using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION
{
    public abstract class State : ScriptableObject
    {
        public virtual void EnterState(Enemy model){}
        public abstract void ExecuteState(Enemy model);
        public virtual void ExitState(Enemy model){}
    }
    
}

