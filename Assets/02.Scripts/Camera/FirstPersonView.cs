using System.Collections;
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

    private Quaternion characterRot;
    private Quaternion cameraRot;

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

    public void Init(Transform target, Transform camera,IUserInputManager userInput)
    {
        characterRot = target.localRotation;
        cameraRot = camera.localRotation;
        UserInput = userInput;
    }

    //as you can see this code is lot like mouseLook code 
    //It is because mouselook code is too fundamental
    public void RotateView(Transform target, Transform camera,Transform neckTransform)
    {
        float yRot = UserInput.GetAxis("Mouse X") * XSensitivity;
        float xRot = UserInput.GetAxis("Mouse Y") * YSensitivity;

        characterRot *= Quaternion.Euler(0f, 0f , yRot);
        cameraRot *= Quaternion.Euler(xRot, 0f, 0f);

        if (IsClampOnRotatingXAxis)
            cameraRot = ClampOnRotatingXAxis(cameraRot);

        if(IsSmooth)
        {
            target.localRotation = Quaternion.Slerp(target.localRotation, characterRot,
                SmoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, cameraRot,
                SmoothTime * Time.deltaTime);
        }
        else
        {
            //target.localRotation = characterRot;
            camera.localRotation = cameraRot;
        }

        var quater = characterRot; 
        //var quater = characterRot * cameraRot;


        //quater *= Quaternion.Euler(0, 0, -quater.eulerAngles.z);

        neckTransform.localRotation = quater;

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

    public void SetCameraPos(Transform target, Transform camera)
    {
        camera.position = new Vector3(target.position.x + OffSetX, target.position.y + OffSetY, target.position.z + OffSetZ);
    }
}
