using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/PlayerDecision/RunDecision")]
public class RunDecison : Decision
{
    public override bool Decide(IStateController controller)
    {
        var playerControl = controller as PlayerStateController;
        if (playerControl.IsRunning)
            return true;
        else
            return false;
    }
}
