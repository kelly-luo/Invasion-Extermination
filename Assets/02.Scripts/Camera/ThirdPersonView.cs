using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonView : ICameraView
{
    public float XSensitivity { get; set; } = 5f;
    public float YSensitivity { get; set; } = 5f;

    public IUserInputManager UserInput { get; set; }
    public Quaternion HeadRot
    {
        get { return headRot; }
        set { headRot = value; }
    }

    private Transform target;
    private Transform neck;
    private Transform head;
    private Transform cameraRig;
    private Transform camera;

    private Quaternion characterRot;
    private Quaternion cameraRot;
    private Quaternion headRot;

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
        this.TargetOffSet = targetOffSet;
        this.Height = height;
        this.Distance = distance;
    }

    public void Init(Transform target, Transform neck, Transform head, Transform cameraRig, Transform camera,  IUserInputManager userInput)
    {
        this.target = target;
        this.neck = neck;
        this.head = head;
        this.cameraRig = cameraRig;
        this.camera = camera;

        this.characterRot = target.localRotation;
        this.cameraRot = camera.localRotation;
        this.cameraLocalPos = camera.localPosition;

        this.UserInput = userInput;
    }

    public void RotateView()
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
