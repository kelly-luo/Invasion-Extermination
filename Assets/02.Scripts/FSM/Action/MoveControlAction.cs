using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu (menuName = "PluggableScript/PlayerAction/MoveControlAction")]
public class MoveControlAction : Action
{

    public override void Act(IStateController controller)
    {
        if (controller is PlayerStateController stateControlller)
        {
            var vertical = stateControlller.UnityService.GetAxis("Vertical");
            var horizontal = stateControlller.UnityService.GetAxis("Horizontal");

            Vector3 moveDir = (stateControlller.CameraRigTr.forward * vertical)
                + (stateControlller.CameraRigTr.right * horizontal);

            stateControlller.PlayerTranslate.TranslateCharacterOnGround(true, moveDir);
            stateControlller.MoveAnimation(horizontal, vertical);

        }
    }
}
    