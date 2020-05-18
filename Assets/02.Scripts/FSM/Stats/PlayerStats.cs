using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;


//[CreateAssetMenu(menuName = "PluggableScript/Stats/PlayerStats")]
[System.Serializable]
public class PlayerStats
{
    public int Level { get; set; }
    public int Score { get; set; }
    public int Money { get; set; }
    public float Health { get; set; }

    public float[] position;

    public PlayerStats(PlayerStateController player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        this.Level = player.Level;
        this.Score = player.Score;
        this.Money = player.Money;
        this.Health = player.Health;
    }

}
