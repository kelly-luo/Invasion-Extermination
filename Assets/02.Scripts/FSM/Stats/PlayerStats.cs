using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;


[CreateAssetMenu(menuName = "PluggableScript/Stats/PlayerStats")]
[System.Serializable]
public class PlayerStats : ObjectStats
{

    [field: SerializeField]
    public int Level { get; set; }
    public float[] position;

    public PlayerStats(PlayerStateController player)
    {
        position = new float[3];
        position[0] = player.Transform.position.x;
        position[1] = player.Transform.position.y;
        position[2] = player.Transform.position.z;
    }

}
