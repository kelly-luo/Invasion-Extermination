using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraMove
{
    void Init(Transform target, Transform neck, Transform head,Transform cameraRig, Transform camera, IUserInputManager userInput);

    void RotateView();

    void SetCameraPos(Transform target, Transform cameraRig);
}
 