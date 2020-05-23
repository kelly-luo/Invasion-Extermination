using UnityEngine;

namespace IEGame.FiniteStateMachine
{
    public interface IStateController
    {

        //stats of something that inherit from ObjectStats


        void TransitionToState(State nextState);

        void TakeDamage(float Damage);

        void OnDeath();
    }
}
