using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;
//This class allows spawn gameobject (projectile prefabs) and throw it using Cubic Bezier curve

[CreateAssetMenu(menuName = "PluggableScript/EnemyAction/ThrowProjectileAction")]
public class ThrowProjectileAction : Action
{

    public GameObject[] projectilePrefabs;

    public float numOfPostionPoint = 50;
    public Vector3 projectilSpawnOffSet = new Vector3(0,40f,0);
    public float projectileSpawnAngle = 90; // 180 = left 0 = right
    public float projectileSpawnDistance = 5f;
    public float initialControlPointDistance = 20f;
    public float targetControlPointDistance = 20f;
    public float delayBetweenShoot = 1f;
    public float StateUpdateDelayTime;

    public float timeSpeedFactor = 0.3f; // delay of translate projectile toward each point of path  

    public override void Act(IStateController controller)
    {
        if (controller is MonsterController monsterController)
        {
            Debug.Log("asdasdasdasdasdasd");
            monsterController.StateUpdateDelayTime = StateUpdateDelayTime;

            monsterController.StartCoroutine(StartThrowNumberOfProjectile(monsterController, 3));

        }
    }

    //calculate the player's position and throw a projectile
    private IEnumerator StartThrowNumberOfProjectile(MonsterController monsterController,int numberOfProjectile)
    {
        Vector3 UnitVectorToTarget = (monsterController.transform.position - monsterController.PlayerTr.position);

        for (int i = 0; i < numberOfProjectile; i++)
        {

            Vector3 projectailSpawnPosition = (monsterController.transform.rotation * new Vector3(projectileSpawnDistance * Mathf.Cos((projectileSpawnAngle ) * Mathf.Deg2Rad)
                ,projectileSpawnDistance * Mathf.Sin((projectileSpawnAngle ) * Mathf.Deg2Rad)
                , 0f))
                + monsterController.transform.position 
                + projectilSpawnOffSet;
            Vector3 initialControlPointPosition = (monsterController.transform.rotation * new Vector3(initialControlPointDistance * Mathf.Cos((projectileSpawnAngle ) * Mathf.Deg2Rad)
                , initialControlPointDistance * Mathf.Sin((projectileSpawnAngle) * Mathf.Deg2Rad)
                , 0f))
                + monsterController.transform.position
                + projectilSpawnOffSet;
            Vector3 targetControlPointPosition = (monsterController.transform.rotation * new Vector3(targetControlPointDistance * Mathf.Cos((projectileSpawnAngle) * Mathf.Deg2Rad)
                , targetControlPointDistance * Mathf.Sin((projectileSpawnAngle ) * Mathf.Deg2Rad)
                , 0f))
                + monsterController.PlayerTr.position;

            monsterController.StartCoroutine(ThrowRandomProjectile(projectailSpawnPosition, monsterController.PlayerTr.position
                , initialControlPointPosition, targetControlPointPosition));

            projectileSpawnAngle = ((projectileSpawnAngle + 30) % 180) ;
            yield return new WaitForSeconds(delayBetweenShoot);

        }
    }

    // give random projectile object in the array
    private GameObject GetRandomProjectile(Vector3 initialPosition)
    {
        //initialPosition include boss mob position + Some Offset
        return GameObject.Instantiate(projectilePrefabs[UnityServiceManager.Instance.UnityRandomRange(0, projectilePrefabs.Length)],
            initialPosition, Quaternion.identity);
    }

    //throw one projectile 
    private IEnumerator ThrowRandomProjectile(Vector3 initialPosition,Vector3 targetPosition,Vector3 intialControlPoint, Vector3 targetControlPoint)
    {
        GameObject projectile = GetRandomProjectile(initialPosition);
        ImProjectile projectileClass = projectile.GetComponent<ImProjectile>();
        if (projectileClass != null)
        {
            float t = 0;
            float eachTimePoint = 1 / numOfPostionPoint;
            while (!projectileClass.IsCollideWithOther && t <= 1)
            {
                yield return new WaitForSeconds(timeSpeedFactor);
                projectile.transform.position = CalculateCubicBezierCurve(t, initialPosition, intialControlPoint, targetControlPoint, targetPosition);
                t += eachTimePoint;
            }
            projectileClass.SetGravity(true);
        }
    }

    //Using Bezier curves (bacially just multiple linear Intrepolation) which t should be value between and include 0 to 1
    private Vector3 CalculateCubicBezierCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

}
    