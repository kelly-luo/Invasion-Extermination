using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Transform> SpawnPoints { get; set; } = new List<Transform>();

    private static GameManager instance = null;

    private float humanCreateTime = 3f;

    private float alienCreateTime = 2f;

    public int numberOfAlien;

    public int numberOfHuman;

    public int experienceScore;

    public float spawnDeley = 2f;

    public int maxHuman = 30;
    public int maxAlien = 5;

    public bool ishand = false;
    private MobFactory EnemyFactory;

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

    private IEnumerator CreateEnemy()
    {
        while(!ClearRound)
        {
            yield return new WaitForSeconds(spawnDeley);
            numberOfAlien = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
            numberOfHuman = (int)GameObject.FindGameObjectsWithTag("Human").Length;
            if (numberOfHuman <= maxHuman )
            {
                SpawnHuman();
                if (isScoreSet)
                    experienceScore += -10;

                if (experienceScore >= requiredScore)
                    ClearRound = true;

            } else if(numberOfAlien <= maxAlien)
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

}