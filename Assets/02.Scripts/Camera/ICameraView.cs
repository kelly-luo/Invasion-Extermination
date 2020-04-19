using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraView
{
    bool IsSmooth { get; set; }
    float SmoothTime { get; set; }
    float XSensitivity { get; set; }
    float YSensitivity { get; set; }
    float XRot { get; }
    float YRot { get; }

    Quaternion CameraRigRot { get; set; }
    Quaternion CameraRot { get; set; }
    

    void Init(Transform target, Transform cameraRig, Transform camera, IUserInputManager userInput);

    void RotateView();

    void SetCameraPos();
    //This returns foward Direction where player looking
    Vector3 GetCameraDirection();
}
 