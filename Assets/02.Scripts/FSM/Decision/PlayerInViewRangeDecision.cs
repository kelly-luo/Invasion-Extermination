using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;


[CreateAssetMenu(menuName = "PluggableScript/EnemyDecision/PlayerInViewRangeDecision")]
public class PlayerInViewRangeDecision : Decision
{
    public override bool Decide(IStateController controller)
    {
        var monsterController = controller as MonsterController;

        if (monsterController.DistancePlayerAndEnemy > monsterController.viewRange)
            return false;
        else
            return true;
    }
}
