using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyDecision/PlayerInAimDecision")]
public class PlayerInAimDecision : Decision
{

    public override bool Decide(IStateController controller)
    {
        bool isView = false;

        var monsterController = controller as MonsterController;
        if (monsterController.DistancePlayerAndEnemy <= monsterController.viewRange)
        {
          
            RaycastHit hit;
            Vector3 dir = (monsterController.PlayerTr.position -
                monsterController.ObjectTransform.position).normalized;

            if (Physics.Raycast(monsterController.ObjectTransform.position,
                monsterController.ObjectTransform.forward, out hit, monsterController.viewRange, monsterController.LayerMask))
            {
                isView = (hit.collider.CompareTag("Player"));
            }
        }

        return isView;
    }
}
