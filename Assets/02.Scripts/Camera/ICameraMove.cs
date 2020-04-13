using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraMove
{
    void Init(Transform target, Transform camera, IUserInputManager userInput);

    void RotateView(Transform target, Transform camera ,Transform neckTransform);

    void SetCameraPos(Transform target, Transform camera);
}
 