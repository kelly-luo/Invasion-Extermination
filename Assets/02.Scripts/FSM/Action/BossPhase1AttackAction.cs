using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/BossPhase1AttackAction")]
public class BossPhase1AttackAction : Action
{
    //ActionList where randomly from thsi list
    public Action[] phaseOneAttackActions;
    

    public override void Act(IStateController controller)
    {
        phaseOneAttackActions[UnityServiceManager.Instance.UnityRandomRange(0,
            phaseOneAttackActions.Length)].Act(controller);
    }


}
