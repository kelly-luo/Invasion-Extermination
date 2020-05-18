using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityServiceManager : IUnityServiceManager
{
    public float DeltaTime
    {
        get { return Time.deltaTime; }
    }

    public float TimeAtFrame
    {
        get { return Time.time; }
    }

    public Vector3 InsideUnitSphere
    {
        get { return Random.insideUnitSphere;  }
    }

    public float GetAxis(string inputKey)
    {
        return Input.GetAxis(inputKey);
    }

    public bool GetKeyUp(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }


    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public bool GetMouseButtonUp(int button)
    {
        return Input.GetMouseButton(button);
    }
}
