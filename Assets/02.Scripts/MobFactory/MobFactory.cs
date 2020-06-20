using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobFactory : MonoBehaviour
{
    private static MobFactory instance = null;

    public static MobFactory Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        //If the instance has different instance of this class, it means new class.
        // if a game flows to next scene and comeback to this scene and Awake() again then delete the new game manager.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public abstract GameObject CreateMob(Vector3 spawnLocation);

    public abstract GameObject CreateMobWithWeapon(Vector3 spawnLocation);

    public abstract GameObject CreateBoss(Vector3 spawnLocation);
}
