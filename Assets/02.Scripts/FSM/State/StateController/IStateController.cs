using UnityEngine;

namespace IEGame.FiniteStateMachine
{
    public interface IStateController
    {

        State CurrentState { get; set; }

        State RemainState { get; set; }

        //stats of something that inherit from ObjectStats
        ObjectStats Stats { get; set; }

        Transform ObjectTransform { get; set; }

        void TransitionToState(State nextState);

        void TakeDamage(float Damage);

        void OnDeath();
    }
}
