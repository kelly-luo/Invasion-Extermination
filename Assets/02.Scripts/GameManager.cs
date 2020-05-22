using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Transform> SpawnPoints { get; set; } = new List<Transform>();

    private static GameManager instance = null;

    private float humanCreateTime = 3f;

    private float alienCreateTime = 2f;

    public static GameManager Instance
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



    // Start is called before the first frame update
    void Start()
    {
        var group = GameObject.Find("EnemySpawnPoints");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(SpawnPoints);
            //removing the waypoint folder
            SpawnPoints.RemoveAt(0);
        }
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
