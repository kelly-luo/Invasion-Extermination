using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[System.Serializable]
[CreateAssetMenu(menuName = "PluggableScript/Stats/PlayerStats")]
public class PlayerStats : ObjectStats
{

    [field: SerializeField]
    public int Level { get; set; }
    public float[] position;

    public PlayerStats(PlayerStateController player)
    {
        position = new float[3];
        //position[0] = player.Character.position.x;
        //position[1] = player.Character.position.y;
        //position[2] = player.Character.position.z;
    }

}
