﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView: ICameraMove
{
    public bool IsClampOnRotatingXAxis { get; set; } = true;
    public float MaxAngleOnRotatingXAxis { get; set; } = 90f;
    public float MinAngleOnRotatingXAxis { get; set; } = -90f;
    public bool IsSmooth { get; set; } = false;
    public float SmoothTime { get; set; } = 5f; 
    public float XSensitivity { get; set; } = 2f;
    public float YSensitivity { get; set; } = 2f;
    public bool IsCursorLocked { get; set; } = true;
    public IUserInputManager UserInput { get; set; }

    private Quaternion targetRot;
    private Quaternion cameraRigRot;
    private Quaternion cameraRot;
    private Quaternion neckRot;

    #region Camera Setting
    public float OffSetX { get; set; }
    public float OffSetY { get; set; }
    public float OffSetZ { get; set; }
    #endregion

    public FirstPersonView(float offSetX = 0, float offSetY = 1.7f, float offSetZ = 0f)
    {
        OffSetX = offSetX;
        OffSetY = offSetY;
        OffSetZ = offSetZ;
    }

    public void Init(Transform target, Transform cameraRig, Transform camera, Transform neck, IUserInputManager userInput)
    {
        targetRot = target.localRotation;
        cameraRigRot = cameraRig.localRotation;
        cameraRot = camera.localRotation;
        neckRot = neck.localRotation;
        UserInput = userInput;
    }

    //as you can see this code is lot like mouseLook code 
    //It is because mouselook code is too fundamental
    public void RotateView(Transform target, Transform cameraRig, Transform camera, Transform neck)
    {
        float yRot = UserInput.GetAxis("Mouse X") * XSensitivity;
        float xRot = UserInput.GetAxis("Mouse Y") * YSensitivity;

        cameraRigRot *= Quaternion.Euler(0, yRot, 0f);
        cameraRot *= Quaternion.Euler(-xRot, 0f, 0f);

        if (IsClampOnRotatingXAxis)
            cameraRot = ClampOnRotatingXAxis(cameraRot);

        if(IsSmooth)
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


        RotateNeckAndTarget(target, neck, yRot, xRot);


    }

    private void RotateNeckAndTarget(Transform target, Transform neck, float yRot, float xRot)
    {
        //you might be wondering why did i use - on yRot and xRot and changed the x to z axis and y to z axis
        //it is because the I found that local Transform of a Neck bone in model was reversed. 
        var nextAngle = neckRot.eulerAngles.x + -yRot;
        if (nextAngle < 180)
        {
            if (nextAngle > 55)
            {
                var leftAngle = (nextAngle - 55);
                var angleUpToLimit = -yRot - leftAngle;
                neckRot *= Quaternion.Euler(angleUpToLimit, 0f, -xRot);
                neckRot *= Quaternion.Euler(0f, -neckRot.eulerAngles.y, 0f);    

                target.localRotation *= Quaternion.Euler(0, yRot, 0f);

            }
            else
            {
                neckRot *= Quaternion.Euler(-yRot, 0f, -xRot);
                neckRot *= Quaternion.Euler(0f, -neckRot.eulerAngles.y, 0f);
            }
        }
        else if (nextAngle > 180)
        {
            if (nextAngle < 305)
            {
                var leftAngle = (nextAngle - 305);
                var angleUpToLimit = -yRot - leftAngle;
                neckRot *= Quaternion.Euler(angleUpToLimit, 0f, -xRot);
                neckRot *= Quaternion.Euler(0f, -neckRot.eulerAngles.y, 0f);

                target.localRotation *= Quaternion.Euler(0, yRot, 0f);

            }
            else
            {
                neckRot *= Quaternion.Euler(-yRot, 0f, -xRot);
                neckRot *= Quaternion.Euler(0f, -neckRot.eulerAngles.y, 0f);
            }
        }

        neck.localRotation = neckRot;

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

    public Vector3 SetCameraPos()
    {
        throw new System.NotImplementedException();
    }

    public void SetCameraPos(Transform target, Transform cameraRig)
    {
        cameraRig.position = new Vector3(target.position.x + OffSetX, target.position.y + OffSetY, target.position.z + OffSetZ);
    }
}
