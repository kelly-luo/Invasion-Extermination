using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonView : ICameraView
{
    public bool IsClampOnRotatingXAxis { get; set; } = true;
    public float MaxAngleOnRotatingXAxis { get; set; } = 80f;
    public float MinAngleOnRotatingXAxis { get; set; } = -90f;
    public bool IsSmooth { get; set; } = true;
    public float SmoothTime { get; set; } = 1f;
    public float XSensitivity { get; set; }
    public float YSensitivity { get; set; }

    public IUserInputManager UserInput { get; set; }

    public float YRot { get; set; }
    public float XRot { get; set; }

    public Quaternion CameraRigRot
    {
        get { return cameraRigRot; }
        set { cameraRigRot = value; }
    }
    public Quaternion CameraRot
    {
        get { return cameraRot; }
        set { cameraRot = value; }
    }

    private Transform target;
    private Transform cameraRig;
    private Transform camera;

    private Quaternion cameraRigRot;
    private Quaternion cameraRot;


    #region Camera Setting
    //offSet (Y) of target
    public float TargetOffSet { get; set; } = 1.65f;
    //distance between a target(Player) 
    public float Distance { get; set; } = 2.0f;
    #endregion

    public ThirdPersonView (float targetOffSet = 2.0f, float distance = 4.0f)
    {
        this.TargetOffSet = targetOffSet;
        this.Distance = distance;
    }

    public void Init(Transform target,Transform cameraRig, Transform camera,  IUserInputManager userInput)
    {
        this.target = target;
        this.cameraRig = cameraRig;
        this.camera = camera;

        this.cameraRigRot = cameraRig.localRotation;
        this.cameraRot = camera.localRotation;
        this.UserInput = userInput;

    }

    public void RotateView()
    {
        YRot = UserInput.GetAxis("Mouse X") * XSensitivity;
        XRot = UserInput.GetAxis("Mouse Y") * YSensitivity;

        cameraRigRot *= Quaternion.Euler(0, YRot, 0f);
        cameraRot *= Quaternion.Euler(-XRot, 0f, 0f);

        if (IsClampOnRotatingXAxis)
            cameraRot = ClampOnRotatingXAxis(cameraRot);

        if (IsSmooth)
        {
            cameraRig.localRotation = Quaternion.Slerp(cameraRig.localRotation, cameraRigRot,
                SmoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, cameraRot,
                SmoothTime * Time.deltaTime);
        }
        else
        {
            cameraRig.localRotation = cameraRigRot;
            camera.localRotation = cameraRot;
        }

    }


    private Quaternion ClampOnRotatingXAxis(Quaternion q)
    {

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;


        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinAngleOnRotatingXAxis, MaxAngleOnRotatingXAxis);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }


    public void SetCameraPos()
    {
        RaycastHit hit;
        var originPoint = (target.position + new Vector3(0f, TargetOffSet, 0f));
        var camPosVect = (-camera.forward);
        //shoot the raycast from the origin and toward the camPoseVect 
        if (Physics.Raycast(originPoint, camPosVect, out hit, Distance))
        {
            //when it hit the coilder it is position of camera.
            cameraRig.position = hit.point;
        }
        else
        {
            cameraRig.position = camPosVect * Distance + originPoint;
        }
    }

    public Vector3 GetCameraDirection()
    {
        return camera.forward;
    }

}
