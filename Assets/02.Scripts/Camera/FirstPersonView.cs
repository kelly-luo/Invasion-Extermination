using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView : ICameraView
{
    public bool IsClampOnRotatingXAxis { get; set; } = true;
    public float MaxAngleOnRotatingXAxis { get; set; } = 80f;
    public float MinAngleOnRotatingXAxis { get; set; } = -90f;
    public float MaxAngleOnHeadRotation { get; set; } = 90f;
    public float MinAngleOnHeadRotation { get; set; } = -90f;
    public bool IsSmooth { get; set; } = false;
    public float SmoothTime { get; set; } = 1f;
    public float XSensitivity { get; set; }
    public float YSensitivity { get; set; }
    public IUserInputManager UserInput { get; set; }

    public Quaternion HeadRot
    {
        get { return headRot; }
        set { headRot = value; }
    }
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
    private Transform neck;
    private Transform head;
    private Transform cameraRig;
    private Transform camera;

    private Quaternion targetRot;
    private Quaternion cameraRigRot;
    private Quaternion cameraRot;
    private Quaternion neckRot;
    private Quaternion headRot;

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


    public void Init(Transform target, Transform neck, Transform head, Transform cameraRig, Transform camera, IUserInputManager userInput)
    {
        this.target = target;
        this.neck = neck;
        this.head = head;
        this.cameraRig = cameraRig;
        this.camera = camera;

        this.targetRot = target.localRotation;
        this.neckRot = neck.localRotation;
        this.headRot = Quaternion.Euler(0f, 0f, 0f);

        this.cameraRigRot = cameraRig.localRotation;
        this.cameraRot = camera.localRotation;
        this.UserInput = userInput;


    }

    //as you can see this code is lot like mouseLook code 
    //It is because mouselook code is too fundamental
    public void RotateView()
    {
        float yRot = UserInput.GetAxis("Mouse X") * XSensitivity;
        float xRot = UserInput.GetAxis("Mouse Y") * YSensitivity;

        cameraRigRot *= Quaternion.Euler(0, yRot, 0f);
        cameraRot *= Quaternion.Euler(-xRot, 0f, 0f);

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


        RotateHeadAndTarget(yRot);
        RotateNeck(xRot);

    }

    private void RotateNeck(float xRot)
    {
        //you might be wondering why did i put the x on z axis and 
        //it is because the I found that local Transform of a Neck bone in model was reversed. 
        neck.localRotation = Quaternion.Euler(0f, 0f, cameraRot.eulerAngles.x);

    }
    //this method need to be more clean
    private void RotateHeadAndTarget (float yRot)
    {
        //NextAngle is amount of angle change in head rotation
        var nextAngle = headRot.eulerAngles.y + yRot;
        if (nextAngle < 180)
        {
            //so it is necessary to seperate the amount of angle that go in to the head rotation 
            //and boby rotation(if the angle exceed maxAngle of Head Rotation)
            if (nextAngle > MaxAngleOnHeadRotation)
            {
                var leftoverAngle = (nextAngle - MaxAngleOnHeadRotation);
                var angleUpToLimit = yRot - leftoverAngle;

                headRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                head.localRotation *= Quaternion.Euler(0, angleUpToLimit, 0);

                target.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0f, yRot, 0f);
                head.localRotation *= Quaternion.Euler(0, yRot, 0);
              
            }
        }
        else if (nextAngle > 180)
        {
            if (nextAngle < (360 + MinAngleOnHeadRotation))
            {
                var leftoverAngle = (nextAngle - (360 + MinAngleOnHeadRotation));
                var angleUpToLimit = yRot - leftoverAngle;
                headRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                head.localRotation *= Quaternion.Euler(0, angleUpToLimit, 0);

                target.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0, yRot, 0);
                head.localRotation *= Quaternion.Euler(0, yRot, 0);
            }
        }

        //if (neckRot.eulerAngles.x +)

        //    float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(neckRot.x);// fix this 
        //angleY = Mathf.Clamp(angleY, -45, 45);
        //Debug.Log(yRot);
        //if (angleY <= -45f)
        //{
        //    if (yRot > 0f)
        //    {
        //        neckRot *= Quaternion.Euler(-yRot, 0f, -xRot);
        //        neckRot *= Quaternion.Euler(0f, -neckRot.eulerAngles.y, 0f);
        //    }
        //    else
        //    {

        //        target.localRotation *= Quaternion.Euler(0, yRot, 0f);
        //    }

        //}
        //else if (angleY >= 45f)
        //{
        //    if (yRot < 0f)
        //    {
        //        neckRot *= Quaternion.Euler(-yRot, 0f, -xRot);
        //        neckRot *= Quaternion.Euler(0f, -neckRot.eulerAngles.y, 0f);
        //    }
        //    else
        //    {
        //        target.localRotation *= Quaternion.Euler(0, yRot, 0f);

        //    }
        //}
        //else
        //{

        //    neckRot *= Quaternion.Euler(-yRot, 0f, -xRot);
        //    neckRot *= Quaternion.Euler(0f, -neckRot.eulerAngles.y, 0f);
        //}
        //neck.localRotation = neckRot;
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
        cameraRig.position = new Vector3(target.position.x + OffSetX, target.position.y + OffSetY, target.position.z + OffSetZ);
    }

    public Vector3 GetCameraDirection()
    {
        return camera.forward; ;
    }
}
