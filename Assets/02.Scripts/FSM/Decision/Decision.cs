using UnityEngine;

namespace IEGame.FiniteStateMachine
{

    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(IStateController controller);
    }
}

 