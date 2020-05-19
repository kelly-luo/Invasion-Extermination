using UnityEngine;

namespace IEGame.FiniteStateMachine
{
    public interface IStateController
    {

        State CurrentState { get; set; }

        State RemainState { get; set; }

        //stats of something that inherit from ObjectStats
        ObjectStats Stats { get; set; }

        Transform PlayerTransform { get; set; }

        float StateTimeElapsed { get; set; }

        void TransitionToState(State nextState);

        bool CheckIsAttackReady(float duration);

        void OnExitState();
    }
}
