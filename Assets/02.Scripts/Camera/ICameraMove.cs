using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraMove
{
    void Init(Transform target, Transform cameraRig, Transform camera, Transform neck, IUserInputManager userInput);

    void RotateView(Transform target, Transform cameraRig, Transform camera, Transform neck);

    void SetCameraPos(Transform target, Transform cameraRig);
}
 