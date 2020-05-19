﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraControl
{
    CameraViewType CurrentViewMode { get; set; }

    float XRot { get; }
    float YRot { get; }

    Action OnViewChange { get; set; }

    Transform GetCameraTransform();

    Vector3 GetSightDirection();

    IEnumerator ShakeCamera(float duration = 0.05f, float magnitudePos = 0.3f, float magnitudeRot = 0.1f);
}
