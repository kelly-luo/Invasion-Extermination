using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/TraceAction")]
public class TraceAction : Action
{
    public override void Act(IStateController controller)
    {
        if (controller is MonsterController monsterController)
        {
            if (monsterController.DistancePlayerAndEnemy > monsterController.viewRange && monsterController.DistancePlayerAndEnemy <= monsterController.failTraceRange)
            {
                monsterController.StateUpdateDelayTime = 0.075f;
                monsterController.TraceTarget = monsterController.PlayerTr.position;
            }
        }
    } 

}

