using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyDecision/PlayerInAimDecision")]
public class PlayerInAimDecision : Decision
{

    public override bool Decide(IStateController controller)
    {

        var monsterController = controller as MonsterController;
        bool isView = false; 
        RaycastHit hit;
        Vector3 dir = (monsterController.PlayerTr.position -
            monsterController.ObjectTransform.position).normalized;

        if (Physics.Raycast(monsterController.ObjectTransform.position,
            monsterController.ObjectTransform.forward, out hit, monsterController.viewRange, monsterController.LayerMask))
        {
            isView = (hit.collider.CompareTag("Player"));
        }

        return isView;
    }
}
