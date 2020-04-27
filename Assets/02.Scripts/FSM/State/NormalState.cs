using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

public class NormalState : State
{
    public Action[] actions;
    public Transition[] transitions;
    

    public override void UpdateState(IStateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    protected override void DoActions(IStateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    protected override void CheckTransitions(IStateController controller)
    {
        for(int i = 0; i <transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);
            
            if(decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                //if the false state is remainState state will not change 
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }


}
