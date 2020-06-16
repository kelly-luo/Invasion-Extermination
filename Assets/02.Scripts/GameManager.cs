using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public List<Transform> SpawnPoints { get; set; } = new List<Transform>();

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get { return instance; }
    }

    [Header("Enemy Create Info")]
    public int numberOfAlien;

    public int numberOfHuman;

    public int experienceScore;

    public float spawnDeley = 2f;

    public int maxHuman = 30;
    public int maxAlien = 5;

    public bool ishand = false;

    [Header("Root Object Pool")]
    public GameObject moneyBillPrefab;
    public int maxMoneyBillPool = 500; //Number limit of Money objects simultaneously allows in one scene.
    public List<GameObject> moneyBillPool = new List<GameObject>();

    public GameObject ammoPrefab;
    public int maxAmmoPool = 500;
    public List<GameObject> ammoPool = new List<GameObject>();

    private MobFactory EnemyFactory;

    [Header("Prefab of Direct Projectile Pool")]
    public GameObject[] directProjectilePrefabs;
    public int numberOfEachDirectProjectile=3;
    private int directProjectileIdx =0;
    public List<GameObject> directProjectilePool = new List<GameObject>();
    [Header("Prefab of Straight Down Projectile Pool")]
    public GameObject[] straightDownProjectilePrefabs;
    public int numberOfEachStraightDownProjectile = 3;
    private int straightDownProjectileIdx = 0;
    public List<GameObject> straightDownProjectilePool = new List<GameObject>();
    [Header("Prefab of Normal Projectile Pool")]
    public GameObject[] normalProjectilePrefabs;
    public int numberOfEachNormalProjectile = 3;
    private int normalProjectileIdx = 0;
    public List<GameObject> normalProjectilePool = new List<GameObject>();
    [Header("Prefab of explosive Projectile Pool")]
    public GameObject[] explosiveProjectilePrefabs;
    public int numberOfEachExplosiveProjectile = 5;
    private int explosiveProjectileIdx = 0;
    public List<GameObject> explosiveProjectilePool = new List<GameObject>();


    public IUnityServiceManager UnityService { get; set; } = UnityServiceManager.Instance;

    private bool isScoreSet;
    private bool clearRound;
    public bool ClearRound
    {
        get { return clearRound; }
        set 
        {
            if(value)
            {

                maxAlien *= 10;
                requiredScore *= 10;
            }
        }
    }

    private int requiredScore = 50;
   
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        //If the instance has different instance of this class, it means new class.
        // if a game flows to next scene and comeback to this scene and Awake() again then delete the new game manager.
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        CreateMoneyBillPooling();
        CreateDirectProjectilePrefabsPooling();
        CreateStraightDownProjectilePrefabsPooling();
        CreateNormalProjectilePrefabsPooling();
        CreateExplosiveProjectilePrefabsPooling();
    }

    void Start()
    {
        EnemyFactory = GetComponent<EnemyMobFactory>();
        var group = GameObject.Find("EnemySpawnPoints");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(SpawnPoints);
            //removing the waypoint folder
            SpawnPoints.RemoveAt(0);
        }

        if (SpawnPoints.Count > 0)
        {
            StartCoroutine(this.CreateEnemy());
        }

    }

    void Update()
    {
    }

    #region Enemy Spawn method
    private IEnumerator CreateEnemy()
    {
        while (!ClearRound)
        {
            yield return new WaitForSeconds(spawnDeley);
            numberOfAlien = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
            numberOfHuman = (int)GameObject.FindGameObjectsWithTag("Human").Length;
            if (numberOfHuman <= maxHuman)
            {
                SpawnHuman();
                if (isScoreSet)
                    experienceScore += -10;

                if (experienceScore >= requiredScore)
                    ClearRound = true;

            }
            else if (numberOfAlien <= maxAlien)
            {
                SpawnAlien();
                if (isScoreSet)
                    experienceScore += 10;
            }
            else
            {
                isScoreSet = true;
                if (experienceScore >= requiredScore)
                    ClearRound = true;
                yield return null;
            }
        }


    }

    void SpawnHuman()
    {
        EnemyFactory.CreateMob(GetRandomSpawnPoint());
    }

    void SpawnAlien()
    {
        EnemyFactory.CreateMobWithWeapon(GetRandomSpawnPoint());
    }

    Vector3 GetRandomSpawnPoint()
    {
        int randomIdx = UnityService.Range(0, SpawnPoints.Count);
        return SpawnPoints[randomIdx].position;
    }

    #endregion

    #region CreatingPooling

    public GameObject GetMoneyBillObject()
    {
        for (int i = 0; i < moneyBillPool.Count; i++)
        {
            if (moneyBillPool[i].activeSelf == false)
            {
                return moneyBillPool[i];
            }
        }
        return null;
    }

    public GameObject GetAmmoObject()
    {
        for (int i = 0; i < ammoPool.Count; i++)
        {
            if (ammoPool[i].activeSelf == false)
            {
                return ammoPool[i];
            }
        }
        return null;
    }

    public void CreateDroppedItemsPool()
    {
        GameObject moneyPools = new GameObject("MoneyBillPools");
        GameObject ammoPools = new GameObject("AmmoPools");

        for (int i = 0; i < maxMoneyBillPool; i++)
        {
            var obj = Instantiate<GameObject>(moneyBillPrefab, moneyPools.transform);
            obj.name = "MoneyBill_" + i.ToString("00");

            obj.SetActive(false);

            moneyBillPool.Add(obj);
        }
    }

    //since every I want show there is alot of projectiles so I iterate the pool list using own index(idx)
    public GameObject GetDirectProjectileObject()
    {
        for (int i = 0; i < directProjectilePool.Count; i++)
        {
            directProjectileIdx %= directProjectilePool.Count;
            if (directProjectilePool[directProjectileIdx].activeSelf == false)
            {
                return directProjectilePool[directProjectileIdx++];
            }
            directProjectileIdx++;
        }
        return null;
    }

    public void CreateDirectProjectilePrefabsPooling()
    {
        GameObject objectPools = new GameObject("DirectProjectilePools");
        int j = 0;
        for (int i = 0; i < numberOfEachDirectProjectile; i++)
            foreach (GameObject prefab in directProjectilePrefabs)
            {
                var obj = Instantiate<GameObject>(prefab, objectPools.transform);
                obj.name = "DirectProjectile_" + j.ToString("00");

                obj.SetActive(false);

                directProjectilePool.Add(obj);
                j++;
            }
    }

    //since every I want show there is alot of projectiles so I iterate the pool list using own index(idx)
    public GameObject GetStraightDownProjectileObject()
    {
        for (int i = 0; i < straightDownProjectilePool.Count; i++)
        {
            straightDownProjectileIdx %= straightDownProjectilePool.Count;
            if (straightDownProjectilePool[straightDownProjectileIdx].activeSelf == false)
            {
                return straightDownProjectilePool[straightDownProjectileIdx++];
            }
            straightDownProjectileIdx++;
        }
        return null;
    }

    public void CreateStraightDownProjectilePrefabsPooling()
    {
        GameObject objectPools = new GameObject("StraightDownProjectilePools");
        int j = 0;
        for (int i = 0; i < numberOfEachStraightDownProjectile; i++)
            foreach (GameObject prefab in straightDownProjectilePrefabs)
            {
                var obj = Instantiate<GameObject>(prefab, objectPools.transform);
                obj.name = "StraightDownProjectile_" + j.ToString("00") ;

                obj.SetActive(false);

                straightDownProjectilePool.Add(obj);
                j++;
            }
    }

    //since every I want show there is alot of projectiles so I iterate the pool list using own index(idx)
    public GameObject GetNormalProjectileObject()
    {
        for (int i = 0; i < normalProjectilePool.Count; i++)
        {
            normalProjectileIdx %= normalProjectilePool.Count;
            if (normalProjectilePool[normalProjectileIdx].activeSelf == false)
            {
                return normalProjectilePool[normalProjectileIdx++];
            }
            normalProjectileIdx++;
        }
        return null;
    }

    public void CreateNormalProjectilePrefabsPooling()
    {
        GameObject objectPools = new GameObject("NormalProjectilePools");
        int j = 0;
        for (int i = 0; i < numberOfEachNormalProjectile; i++)
            foreach (GameObject prefab in normalProjectilePrefabs)
            {
                var obj = Instantiate<GameObject>(prefab, objectPools.transform);
                obj.name = "NormalProjectile_" + j.ToString("00");

                obj.SetActive(false);

                normalProjectilePool.Add(obj);
                j++;
            }
    }
    //since every I want show there is alot of projectiles so I iterate the pool list using own index(idx)
    public GameObject GetExplosiveProjectileObject()
    {
        for (int i = 0; i < explosiveProjectilePool.Count; i++)
        {
            explosiveProjectileIdx %= explosiveProjectilePool.Count;
            if (explosiveProjectilePool[explosiveProjectileIdx].activeSelf == false)
            {
                return explosiveProjectilePool[explosiveProjectileIdx++];
            }
            explosiveProjectileIdx++;
        }
        return null;
    }
    public void CreateExplosiveProjectilePrefabsPooling()
    {
        GameObject objectPools = new GameObject("ExplosiveProjectilePools");
        int j = 0;
        for (int i = 0; i < numberOfEachExplosiveProjectile; i++)
            foreach (GameObject prefab in explosiveProjectilePrefabs)
            {
                var obj = Instantiate<GameObject>(prefab, objectPools.transform);
                obj.name = "ExplosiveProjectile_" + j.ToString("00");

                obj.SetActive(false);

                explosiveProjectilePool.Add(obj);
                j++;
            }
    }


    #endregion

}