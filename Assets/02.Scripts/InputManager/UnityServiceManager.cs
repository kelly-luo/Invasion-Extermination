using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
/// <summary>
/// This is  class to use the Service that Unity provided for us
/// This is Singleton.
/// </summary>
public class UnityServiceManager : IUnityServiceManager
{
    private static UnityServiceManager instance;
    public static UnityServiceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UnityServiceManager();
            }

            return instance;
        }
    }

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
        get { return Random.insideUnitSphere; }
    }

    public Vector3 GetMainCameraPosition {
       get { return Camera.main.transform.position; }
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

    public int Range(int min, int max)
    {
        return Random.Range(min, max);
    }

    public Collider[] OverlapSphere(Vector3 position, float viewRange, int layerMask)
    {
        return Physics.OverlapSphere(position, viewRange, layerMask);
    }

    public Vector3 WorldSpaceToScreenSpace(Vector3 point)
    {
        return Camera.main.WorldToScreenPoint(point);
    }

}
