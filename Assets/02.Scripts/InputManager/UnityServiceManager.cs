using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class UnityServiceManager : IUnityServiceManager
{
    private static UnityServiceManager instance;
    public static UnityServiceManager Instance
    {
        get
        {
            if(instance == null)
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

    public int UnityRandomRange(int min, int max)
    {
        return Random.Range(min, max);
    }

    public float UnityRandomGaussian(float mean, float standDev)
    {
        float[] points = new float[100];
        float min = mean;
        float max = standDev * 3f;
        float range = max - min;
        float buckets = range / 20f;

        for(int i = 0; i<20; i++)
        {
            for(int j = 0; j<5; j++)
            {
                points[j + i*5] = Random.Range(min, min + ((i+1) * buckets));
            }
        }
        return points[Random.Range(0, 100)];
    }

    public int Range(int min, int max)
    {
        return Random.Range(min, max);
    }

    public Collider[] OverlapSphere(Vector3 position, float viewRange, int layerMask)
    {
        return Physics.OverlapSphere(position, viewRange, layerMask);
    }
}
