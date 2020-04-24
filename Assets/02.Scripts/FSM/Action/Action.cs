using UnityEngine;

namespace IEGame.FiniteStateMachine
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(IStateController controller);
    }
}

