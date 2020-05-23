using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyDecision/PlayerInTraceRangeDecision")]
public class PlayerInTraceRangeDecision : Decision
{
    public override bool Decide(IStateController controller)
    {
        var monsterController = controller as MonsterController;

        if (monsterController.DistancePlayerAndEnemy > monsterController.failTraceRange)
        {
            Debug.Log("PlayerInTraceRangeDecision return: " + false);
            return false;
        }
        else
        {
            Debug.Log("PlayerInTraceRangeDecision return: " + true);
            return true;
        }
    }
}
