using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/PlayerDecision/BossHealthCheckDecision")]
public class BossHealthCheck : Decision
{
    public float phaseTwoHealthEnterPoint = 5000f;
    public override bool Decide(IStateController controller)
    {
        if (controller is MonsterController monsterController)
        {
            if(monsterController.Stats.Health < phaseTwoHealthEnterPoint)
            {
                return true;
            }
           
        }
        return false;
    }
}
