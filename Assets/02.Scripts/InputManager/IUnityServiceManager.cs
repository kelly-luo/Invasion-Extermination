using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnityServiceManager 
{
    int Range(int max, int min);

    float GetAxis(string inputKey);

    bool GetKeyUp(KeyCode key);

    bool GetKeyDown(KeyCode key);

    bool GetMouseButtonUp(int button);
        
    float DeltaTime { get; }

    int UnityRandomRange(int min, int max);

    float TimeAtFrame { get; }

    Vector3 InsideUnitSphere { get; }

    Collider[] OverlapSphere(Vector3 position, float viewRange, int layerMask);

    Vector3 WorldSpaceToScreenSpace(Vector3 point);

    Vector3 GetMainCameraPosition { get; }
}
