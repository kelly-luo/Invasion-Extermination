using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/RotationToPlayerAction")]
public class RotationToPlayerAction : Action
{
    public override void Act(IStateController controller)
    {
        if (controller is MonsterController monsterController)
        {
            if (monsterController.DistancePlayerAndEnemy <= monsterController.viewRange)
            {
                monsterController.RotateToPlayer();
            }
        }
    }
}
