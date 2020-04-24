using UnityEngine;
namespace IEGame.FiniteStateMachine
{
    public abstract class State: ScriptableObject
    {
        
        public abstract void UpdateState(IStateController controller);

        protected abstract void DoActions(IStateController controller);

        protected abstract void CheckTransitions(IStateController controller);
    }
}