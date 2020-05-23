using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/RotationToPlayerAction")]
public class RotationToPlayerAction : Action
{
    public override void Act(IStateController controller)
    {
        var monsterController = controller as MonsterController;

        if( monsterController.DistancePlayerAndEnemy <= monsterController.viewRange)
        {
            monsterController.RotateToPlayer();

        }

        Debug.Log("Ratate Action");
    }
}
