    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/BossMoveAction")]
public class BossMoveAction : Action
{
    public float speed = 10f;

    public override void Act(IStateController controller)
    {
        if (controller is MonsterController monsterController)
        {
            monsterController.Patrolling = true;
            monsterController.Agent.speed = this.speed;
            monsterController.ObjectTransform.rotation = Quaternion.LookRotation(monsterController.PlayerTr.position);
        }
    }
}
