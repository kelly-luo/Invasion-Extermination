using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonView : ICameraMove
{
    public float XSensitivity { get; set; } = 5f;
    public float YSensitivity { get; set; } = 5f;

    public IUserInputManager UserInput { get; set; }

    private Quaternion characterRot;
    private Quaternion cameraRot;
    private Vector3 cameraLocalPos;

    #region Camera Setting
    //offSet (Y) of target
    public float TargetOffSet { get; set; }  = 2.0f;
    //height different between a target(player)
    public float Height { get; set; } = 0.0f;
    //distance between a target(Player) 
    public float Distance { get; set; } = 4.0f;
    #endregion

    public ThirdPersonView (float targetOffSet = 2.0f, float height = 0f, float distance = 4.0f)
    {
        TargetOffSet = targetOffSet;
        Height = height;
        Distance = distance;
    }

    public void Init(Transform target, Transform cameraRig, Transform camera, Transform neck, IUserInputManager userInput)
    {
        characterRot = target.localRotation;
        cameraRot = camera.localRotation;
        cameraLocalPos = camera.localPosition;

        UserInput = userInput;
    }

    public void RotateView(Transform target, Transform cameraRig, Transform camera, Transform neck)
    {
        float xRot = UserInput.GetAxis("Mouse X") * XSensitivity;
        float yRot = UserInput.GetAxis("Mouse Y") * YSensitivity;
    }

    public Vector3 SetCameraPos()
    {
        throw new System.NotImplementedException();
    }

    public void SetCameraPos(Transform target, Transform camera)
    {
        throw new System.NotImplementedException();
    }
}
