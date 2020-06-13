using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;
//This class allows spawn gameobject (projectile prefabs) and throw it using Cubic Bezier curve

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/ThrowExplosiveProjectileAction")]
public class ThrowExplosiveProjectileAction : Action
{
    public float numOfPostionPoint = 50;
    public Vector3 projectilSpawnOffSet = new Vector3(0, 2f, 0);
    public float projectileSpawnAngle = 90; // 180 = left 0 = right
    public float projectileSpawnDistance = 5f;
    public float initialControlPointDistance = 15f;
    public float targetControlPointDistance = 2f;
    public float delayBetweenShoot = 0.75f;
    public float timeSpeedFactor = 0.003f; // delay of translate projectile toward each point of path  


    public int numberOfProjectileToThrow = 3;
    public float stateUpdateDelayTime = 3f;

    public override void Act(IStateController controller)
    {
        if (controller is MonsterController monsterController)
        {
            if (monsterController.ProjectileManager != null)
            {
                monsterController.StateUpdateDelayTime = stateUpdateDelayTime;
                monsterController.ProjectileManager.SetProjectileValue(numOfPostionPoint, projectilSpawnOffSet
                    , projectileSpawnAngle, projectileSpawnDistance, initialControlPointDistance, targetControlPointDistance
                    , delayBetweenShoot, timeSpeedFactor);

                monsterController.ProjectileManager.StartThrowNumberOfExplosiveProjectile(monsterController.PlayerTr.position
                    , numberOfProjectileToThrow);
            }
        }
    }


}
