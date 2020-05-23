using NSubstitute.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform player { get; set; }
    public List<Transform> SpawnPoints { get; set; } = new List<Transform>();
    public IUnityServiceManager UnityService { get; set; }

    public void Start()
    {
        Initialize();
    }

    public Spawn()
    {
        Initialize();
    }

    public void Initialize()
    {
        UnityService = new UnityServiceManager();
        player = GameObject.Find("Player").transform;

        var group = GameObject.Find("RespawnPointsGroup");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(SpawnPoints);
            SpawnPoints.RemoveAt(0);
        }
        SetSpawn();
    }

    public void SetSpawn()
    {
        player.transform.position = SelectSpawnPoint().position;
    }

    public Transform SelectSpawnPoint()
    {
        return SpawnPoints[UnityService.UnityRandomRange(0, SpawnPoints.Count)];
    }

}
