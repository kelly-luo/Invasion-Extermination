using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

public class PlayerStats : ObjectStats
{
    [field: SerializeField]
    public float HP{ get; set; }

    [field: SerializeField]
    public int Level { get; set; }
}
