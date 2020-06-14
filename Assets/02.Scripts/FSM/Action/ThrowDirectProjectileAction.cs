using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;
//This class allows spawn gameobject (projectile prefabs) and throw it using Cubic Bezier curve

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/ThrowDirectProjectileAction")]
public class ThrowDirectProjectileAction : Action
{
    public float numOfPostionPoint = 50;
    public float delayBetweenShoot = 0.75f;
    public float timeSpeedFactor = 0.03f; // delay of translate projectile toward each point of path  
    public int numberOfProjectileToThrow = 1;
    public float stateUpdateDelayTime = 2.5f;
    public float directProjectileSpawnRadius = 25f;

    public override void Act(IStateController controller)
    {
        if (controller is MonsterController monsterController)
        {
            if (monsterController.ProjectileManager != null)
            {
                monsterController.StateUpdateDelayTime = stateUpdateDelayTime;
                monsterController.ProjectileManager.TimeSpeedFactor = timeSpeedFactor;
                monsterController.ProjectileManager.DelayBetweenShoot = delayBetweenShoot;
                monsterController.ProjectileManager.DirectProjectileSpawnRadius = directProjectileSpawnRadius;
                monsterController.TriggerThrowDirectAttack();
                monsterController.ProjectileManager.StartThrowNumberOfDirectProjectile(monsterController.PlayerTr.position
                    ,numberOfProjectileToThrow);
            }
        }
    }


}
