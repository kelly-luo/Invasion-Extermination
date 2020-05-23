    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/PatrolAction")]
public class PatrolAction : Action
{
    public override void Act(IStateController controller)
    {
        var monsterController = controller as MonsterController;
        monsterController.Patrolling = true;
    }
}
