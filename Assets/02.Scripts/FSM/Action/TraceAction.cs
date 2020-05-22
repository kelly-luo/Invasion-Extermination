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

        monsterController.TraceTarget = monsterController.PlayerTr.position;
    }
}
