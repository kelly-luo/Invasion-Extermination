using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityServiceManager : IUnityServiceManager
{
    public float DeltaTime
    {
        get { return Time.deltaTime; }
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

    public int UnityRandomRange(int min, int max)
    {
        return Random.Range(min, max);
    }

}
