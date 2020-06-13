using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;
//action of falling projectile that spawn above player
[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/ThrowStraightDownProjectileAction")]
public class ThrowStraightDownProjectileAction : Action
{


    public float numOfPostionPoint = 500;
    public float delayBetweenShoot = 10f;
    public float timeSpeedFactor = 0.03f; // delay of translate projectile toward each point of path  
    public int numberOfProjectileToThrow = 1;

    public float stateUpdateDelayTime = 6f;
    public float straightDownProjectailSpawn = 50f;
    public override void Act(IStateController controller)
    {
        if (controller is MonsterController monsterController)
        {
            if (monsterController.ProjectileManager != null)
            {
                monsterController.StateUpdateDelayTime = stateUpdateDelayTime;
                monsterController.ProjectileManager.TimeSpeedFactor = timeSpeedFactor;
                monsterController.ProjectileManager.DelayBetweenShoot = delayBetweenShoot;
                monsterController.ProjectileManager.StraightDownProjectailSpawn = straightDownProjectailSpawn;

                monsterController.ProjectileManager.StartThrowNumberOfStraightDownProjectile(monsterController.PlayerTr.position
                    ,numberOfProjectileToThrow);
            }
        }
    }
}
