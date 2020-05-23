using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/TraceAction")]
public class TraceAction : Action
{
    public override void Act(IStateController controller)
    {
        var monsterController = controller as MonsterController;

        if (monsterController.DistancePlayerAndEnemy > monsterController.viewRange && monsterController.DistancePlayerAndEnemy <= monsterController.failTraceRange)
        {
            Debug.Log("Trace Action");
            monsterController.TraceTarget = monsterController.PlayerTr.position;
        }
    }
}
