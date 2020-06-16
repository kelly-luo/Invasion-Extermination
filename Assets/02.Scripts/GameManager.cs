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

    private int roundNo = 1;
    public int RoundNo
    {
        get { return roundNo; }
    }
    public const int maxRound = 10;

    public int maxHuman = 30;
    public int maxAlien = 5;
    private const int LOGVALUESPAWNRATE = 20;

    public bool ishand = false;

    [Header("Object Pool")]
    public GameObject moneyBillPrefab;
    public int maxMoneyBillPool = 500; //Number limit of Money objects simultaneously allows in one scene.
    public List<GameObject> moneyBillPool = new List<GameObject>();

    private MobFactory EnemyFactory;

    public IUnityServiceManager UnityService { get; set; } = UnityServiceManager.Instance;

    private bool isScoreSet;
    private bool clearRound;


 
    private const int LOGVALUESCORE = 50;
    private int requiredScore = 50;
    public int RequiredScore
    {
        get { return requiredScore; }
    }

    public bool ClearRound
    {
        get { return clearRound; }
        set 
        {
            if(value)
            {
                roundNo++;
                maxAlien = (int) CalculateLog(maxAlien, LOGVALUESPAWNRATE); //Increase Enemy size logarithmically 
                maxHuman = (int)CalculateLog(maxHuman , LOGVALUESPAWNRATE); //Increase Enemy size logarithmically 
                requiredScore =(int) CalculateLog(requiredScore,LOGVALUESCORE); //Increase round score requirement logarithmically 
                if (roundNo == maxRound) SpawnBoss();
            }
            
        }
    }

    private float CalculateLog(int original, int log)
    {
        return (float)original * Mathf.Log10(log);
    }

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
        
    }
    // Start is called before the first frame update
    // Start is called before the first frame update
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
            }
            else if (numberOfAlien <= maxAlien)
            {
                SpawnAlien();
            }
            else
            {
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

    void SpawnBoss()
    {
        //Put Spawn for boss here :)
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

    public void CreateMoneyBillPooling()
    {
        GameObject objectPools = new GameObject("MoneyBillPools");

        for (int i = 0; i < maxMoneyBillPool; i++)
        {
            var obj = Instantiate<GameObject>(moneyBillPrefab, objectPools.transform);
            obj.name = "MoneyBill_" + i.ToString("00");

            obj.SetActive(false);

            moneyBillPool.Add(obj);
        }
    }
    #endregion

}