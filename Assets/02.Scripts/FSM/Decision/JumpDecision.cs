using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;


[CreateAssetMenu(menuName = "PluggableScript/PlayerDecision/JumpDecision")]
public class JumpDecision : Decision
{
    public override bool Decide(IStateController controller)
    {
        return false;
    }
}
