using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView : ICameraView
{
    public bool IsClampOnRotatingXAxis { get; set; } = true;
    public float MaxAngleOnRotatingXAxis { get; set; } = 80f;
    public float MinAngleOnRotatingXAxis { get; set; } = -90f;

    public bool IsSmooth { get; set; } = false;
    public float SmoothTime { get; set; } 
    public float XSensitivity { get; set; } 
    public float YSensitivity { get; set; }
    public IUnityServiceManager UnityService { get; set; }
        
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
    public float OffSetX { get; set; }
    public float OffSetY { get; set; }
    public float OffSetZ { get; set; }
    #endregion

    public FirstPersonView(float offSetX = 0, float offSetY = 1.7f, float offSetZ = 0f)
    {
        this.OffSetX = offSetX;
        this.OffSetY = offSetY;
        this.OffSetZ = offSetZ;
    }


    public void Init(Transform target, Transform cameraRig, Transform camera, IUnityServiceManager userInput)
    {
        this.target = target;
        this.cameraRig = cameraRig;
        this.camera = camera;

        this.cameraRigRot = cameraRig.localRotation;
        this.cameraRot = camera.localRotation;
        this.UnityService = userInput;
    }

    //as you can see this code is lot like mouseLook code 
    //It is because mouselook code is too fundamental
    public void RotateView()
    {
        YRot = UnityService.GetAxis("Mouse X") * XSensitivity;
        XRot = UnityService.GetAxis("Mouse Y") * YSensitivity;

        cameraRigRot *= Quaternion.Euler(0, YRot, 0f);
        cameraRot *= Quaternion.Euler(-XRot, 0f, 0f);

        if (IsClampOnRotatingXAxis)
            cameraRot = ClampOnRotatingXAxis(cameraRot);

        if (IsSmooth)
        {
            cameraRig.localRotation = Quaternion.Slerp(cameraRig.localRotation, cameraRigRot,
                SmoothTime * UnityService.DeltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, cameraRot,
                SmoothTime * UnityService.DeltaTime);
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
        //cameraRig.position = new Vector3(target.position.x + OffSetX, target.position.y + OffSetY, target.position.z + OffSetZ);
        cameraRig.position = new Vector3(target.position.x + OffSetX, target.position.y + OffSetY, target.position.z);
        cameraRig.position += cameraRig.forward * OffSetZ;
  
    }

    public Vector3 GetCameraDirection()
    {
        return camera.forward;
    }
}
