using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyDecision/PlayerFoundDecision")]
public class PlayerFoundDecision : Decision
{
    public override bool Decide(IStateController controller)
    {
        var monsterController = controller as MonsterController;

        bool isPlayerFound = false;

        Collider[] colliders = monsterController.UnityService
            .OverlapSphere(monsterController.ObjectTransform.position,
            monsterController.viewRange, 1 << monsterController.PlayerLayer);

        if(colliders.Length == 1) //it means player found
        {

            Vector3 dir = (monsterController.PlayerTr.position
                - monsterController.ObjectTransform.position).normalized;

            if (Vector3.Angle(monsterController.ObjectTransform.forward, dir) <
                monsterController.viewAngle * 0.5f)
            {
                isPlayerFound = true;
            }
        }

        return isPlayerFound;
    }
}
