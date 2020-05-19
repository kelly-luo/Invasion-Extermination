using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/Stats/PlayerStats")]
public class PlayerStats : ObjectStats
{
    float health = 100;

    [field: SerializeField]
    public int Level { get; set; }
}
