using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This component class allows to Manage and Throw projectile for each shot 
public class ProjectileManager : MonoBehaviour
{
    public float NumOfPostionPoint { get; set; } = 50;
    public Vector3 ProjectilSpawnOffSet { get; set; } = new Vector3(0, 2f, 0);
    public float ProjectileSpawnAngle { get; set; } = 90; // 180 = left 0 = right
    public float ProjectileSpawnDistance { get; set; } = 5f;
    //higher the value higher the rising angle 
    public float InitialControlPointDistance { get; set; } = 15f;
    //higher the value higher the descent angle 
    public float TargetControlPointDistance { get; set; } = 2f;

    public float DirectProjectileSpawnRadius { get; set; } = 25f;
    public float StraightDownProjectailSpawn { get; set; } = 50f;
    public float DelayBetweenShoot { get; set; } = 0.75f;
    //speed of projectile move to each point of path
    public float TimeSpeedFactor { get; set; } = 0.003f;

    public bool IsProjectailPathVisible { get; set; } = true;

    public void SetProjectileValue(float numOfPostionPoint ,Vector3 projectilSpawnOffSet
        ,float projectileSpawnAngle ,float projectileSpawnDistance ,float initialControlPointDistance
        ,float targetControlPointDistance ,float delayBetweenShoot,float timeSpeedFactor)
    {
        NumOfPostionPoint = numOfPostionPoint;
        ProjectilSpawnOffSet = projectilSpawnOffSet;
        ProjectileSpawnAngle = projectileSpawnAngle;
        ProjectileSpawnDistance = projectileSpawnDistance;
        InitialControlPointDistance = initialControlPointDistance;
        TargetControlPointDistance = targetControlPointDistance;

        DelayBetweenShoot = delayBetweenShoot;
        TimeSpeedFactor = timeSpeedFactor;
    }
    

    //Wrapped the corutine code so other Non MonoBehaviour class can call without referencing extra Unity Library
    public void StartThrowNumberOfProjectile(Vector3 targetPosition, int numberOfProjectile, GameObject[] projectilePrefabs)
    {
        StartCoroutine(ThrowNumberOfProjectile(targetPosition,numberOfProjectile, projectilePrefabs));
    }


    public void StartThrowNumberOfStraightDownProjectile(Vector3 targetPosition, int numberOfProjectile, GameObject[] projectilePrefabs)
    {
        StartCoroutine(ThrowNumberOfStraightDownProjectile(targetPosition, numberOfProjectile, projectilePrefabs));
    }

    //calculate the player's position and throw a projectile but each proejectile have different path and spawn point 
    private IEnumerator ThrowNumberOfProjectile(Vector3 targetPosition, int numberOfProjectile,GameObject[] projectilePrefabs)
    {

        for (int i = 0; i < numberOfProjectile; i++)
        {
            var randomOffset = new Vector3(UnityServiceManager.Instance.UnityRandomRange(-20,20)* 0.1f
                ,0f
                , UnityServiceManager.Instance.UnityRandomRange(-20, 20) * 0.1f);
            Vector3 projectailSpawnPosition = (gameObject.transform.rotation * new Vector3(ProjectileSpawnDistance * Mathf.Cos((ProjectileSpawnAngle) * Mathf.Deg2Rad)
                , ProjectileSpawnDistance * Mathf.Sin((ProjectileSpawnAngle) * Mathf.Deg2Rad)
                , 0f))
                + gameObject.transform.position
                + ProjectilSpawnOffSet;
            Vector3 initialControlPointPosition = (gameObject.transform.rotation * new Vector3(InitialControlPointDistance * Mathf.Cos((ProjectileSpawnAngle) * Mathf.Deg2Rad)
                , InitialControlPointDistance * Mathf.Sin((ProjectileSpawnAngle) * Mathf.Deg2Rad)
                , 0f))
                + gameObject.transform.position
                + ProjectilSpawnOffSet;
            Vector3 targetControlPointPosition = (gameObject.transform.rotation * new Vector3(TargetControlPointDistance * Mathf.Cos((ProjectileSpawnAngle) * Mathf.Deg2Rad)
                , TargetControlPointDistance * Mathf.Sin((ProjectileSpawnAngle) * Mathf.Deg2Rad)
                , 0f))
                + targetPosition;

            StartCoroutine(ThrowRandomProjectile(projectailSpawnPosition, targetPosition + randomOffset
                , initialControlPointPosition, targetControlPointPosition, projectilePrefabs));

            ProjectileSpawnAngle = ((ProjectileSpawnAngle + 30) % 180);
            yield return new WaitForSeconds(DelayBetweenShoot);

        }
    }
    // give random projectile with given initial position
    private GameObject GetInstantiateProjectile(Vector3 initialPosition, GameObject[] proejectailPrefabs)
    {
        //initialPosition include boss mob position + Some Offset
        return GameObject.Instantiate(proejectailPrefabs[UnityServiceManager.Instance.UnityRandomRange(0, proejectailPrefabs.Length)],
            initialPosition, Quaternion.identity);
    }
    //Wrapped the corutine code so other Non MonoBehaviour class can call without referencing extra Unity Library
    public void StartThrowNumberOfDirectProjectile(Vector3 targetPosition, int numberOfProjectile, GameObject[] projectilePrefabs)
    {
        StartCoroutine(ThrowNumberOfDirectProjectile(targetPosition, numberOfProjectile, projectilePrefabs));
    }

    //calculate the player's position and throw a projectile but each proejectile have different path and spawn point 
    private IEnumerator ThrowNumberOfDirectProjectile(Vector3 targetPosition, int numberOfProjectile, GameObject[] projectilePrefabs)
    {

        for (int i = 0; i < numberOfProjectile; i++)
        {
            //
            var randomOffset =Quaternion.AngleAxis(UnityServiceManager.Instance.UnityRandomRange(0,180),Vector3.up) * new Vector3(0f
                , 0f
                , DirectProjectileSpawnRadius);

            StartCoroutine(ThrowRandomProjectile(randomOffset + targetPosition, -randomOffset + targetPosition
                , randomOffset + targetPosition, -randomOffset + targetPosition, projectilePrefabs));


            yield return new WaitForSeconds(DelayBetweenShoot);

        }
    }
    //calculate the player's position and spawn projectile and each proejectile go straight down
    private IEnumerator ThrowNumberOfStraightDownProjectile(Vector3 targetPosition, int numberOfProjectile, GameObject[] projectilePrefabs)
    {

        for (int i = 0; i < numberOfProjectile; i++)
        {
            var OffSet = new Vector3(0f, StraightDownProjectailSpawn, 0f);
            var EndOffSet = new Vector3(0f, StraightDownProjectailSpawn - 20, 0f);
            StartCoroutine(ThrowRandomProjectile(OffSet + targetPosition, -EndOffSet + targetPosition
                , OffSet + targetPosition, -EndOffSet + targetPosition, projectilePrefabs));


            yield return new WaitForSeconds(DelayBetweenShoot);

        }
    }
    //throw one projectile using CalculateCubicBezierCurve path
    private IEnumerator ThrowRandomProjectile(Vector3 initialPosition, Vector3 targetPosition, Vector3 intialControlPoint, Vector3 targetControlPoint, GameObject[] projectilePrefabs)
    {
        var projectile = GetInstantiateProjectile(initialPosition, projectilePrefabs);
        ImProjectile projectileClass = projectile.GetComponent<ImProjectile>();
        if (projectileClass != null)
        {
            if(projectileClass is DirectProjectile)
                projectile.transform.LookAt(targetPosition);
            if(IsProjectailPathVisible)
                DrawProjectailPathLine(initialPosition, targetPosition, intialControlPoint, targetControlPoint, projectile);

            float t = 0;
            float eachTimePoint = 1 / NumOfPostionPoint;
            while (!projectileClass.IsCollideWithOther && t <= 1)
            {
                yield return new WaitForSeconds(TimeSpeedFactor);
                projectile.transform.position = CalculateCubicBezierCurve(t, initialPosition, intialControlPoint, targetControlPoint, targetPosition);
                t += eachTimePoint;
            }
            projectileClass.AfterThrow();
        }
    }

    private void DrawProjectailPathLine(Vector3 initialPosition, Vector3 targetPosition, Vector3 intialControlPoint, Vector3 targetControlPoint, GameObject projectile)
    {
        var lineRenderer = projectile.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            for (int i = 0; i < 100; i++)
            {
                lineRenderer.SetPosition(i, CalculateCubicBezierCurve(i * 0.01f, initialPosition, intialControlPoint, targetControlPoint, targetPosition));
            }
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
