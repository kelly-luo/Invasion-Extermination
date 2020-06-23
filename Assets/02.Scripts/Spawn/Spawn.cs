//
// Spawn class
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// When a player dies, they randomly spawn at 1 point out of a List of points. These points
// are intialized as long as the GameObject with the respawn points is named "RespawnPointsGroup". 
//
using System.Collections.Generic;
using UnityEngine;
public class Spawn
{
    public Transform player { get; set; }
    public List<Transform> SpawnPoints { get; set; } = new List<Transform>();
    public IUnityServiceManager UnityService { get; set; }

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
