using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraControl
{

    float XRot { get; }
    float YRot { get; }

    Transform GetCameraTransform();

    Vector3 GetSightDirection();
}
