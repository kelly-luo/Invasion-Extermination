    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/PatrolAction")]
public class PatrolAction : Action
{
    public override void Act(IStateController controller)
    {
        if (controller is MonsterController monsterController)
        {
            monsterController.StateUpdateDelayTime = 0.075f;
            monsterController.Patrolling = true;
        }
    }
}
