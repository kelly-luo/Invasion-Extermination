using System.Collections;
using UnityEngine;

public class GameRounds : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING}

    [System.Serializable]
    public class Round
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Round[] rounds;
    private int nextRound = 0;

    public float countdown;

    private SpawnState state = SpawnState.SPAWNING;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                // Start spawning enemies
                StartCoroutine(StartRound(rounds[nextRound]));
            }
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator StartRound(Round round)
    {
        state = SpawnState.SPAWNING;

        //Spawn
        for(int i = 0; i < round.count; i++)
        {
            SpawnEnemy(round.enemy);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }
}
