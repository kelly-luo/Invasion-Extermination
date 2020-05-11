using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/Stats/PlayerStats")]
public class PlayerStats : ObjectStats
{

    [field: SerializeField]
    public int Level { get; set; }
}
