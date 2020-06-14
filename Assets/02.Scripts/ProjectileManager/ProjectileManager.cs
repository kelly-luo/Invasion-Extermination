using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This component class allows to Manage and Throw projectile for each shot 
public class ProjectileManager : MonoBehaviour
{
    public int NumOfPostionPoint { get; set; } = 50;
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

    public bool IsProjectilePathVisible { get; set; } = true;

    //this value should be under 100 or it will do nothing
    public int PreDrawingAmount { get; set; } = 40;

    public void SetProjectileValue(float numOfPostionPoint ,Vector3 projectilSpawnOffSet
        ,float projectileSpawnAngle ,float projectileSpawnDistance ,float initialControlPointDistance
        ,float targetControlPointDistance ,float delayBetweenShoot,float timeSpeedFactor)
    {
        NumOfPostionPoint = (int)numOfPostionPoint;
        ProjectilSpawnOffSet = projectilSpawnOffSet;
        ProjectileSpawnAngle = projectileSpawnAngle;
        ProjectileSpawnDistance = projectileSpawnDistance;
        InitialControlPointDistance = initialControlPointDistance;
        TargetControlPointDistance = targetControlPointDistance;

        DelayBetweenShoot = delayBetweenShoot;
        TimeSpeedFactor = timeSpeedFactor;
    }
    

    //Wrapped the corutine code so other Non MonoBehaviour class can call without referencing extra Unity Library
    public void StartThrowNumberOfProjectile(Vector3 targetPosition, int numberOfProjectile)
    {
        StartCoroutine(ThrowNumberOfProjectile(targetPosition,numberOfProjectile));
    }


    //calculate the player's position and throw a projectile but each proejectile have different path and spawn point 
    private IEnumerator ThrowNumberOfProjectile(Vector3 targetPosition, int numberOfProjectile)
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
                , initialControlPointPosition, targetControlPointPosition, GameManager.Instance.GetNormalProjectileObject()));

            ProjectileSpawnAngle = ((ProjectileSpawnAngle + 30) % 180);
            yield return new WaitForSeconds(DelayBetweenShoot);

        }
    }
    public void StartThrowNumberOfExplosiveProjectile(Vector3 targetPosition, int numberOfProjectile)
    {
        StartCoroutine(ThrowNumberOfExplosiveProjectile(targetPosition, numberOfProjectile));
    }

    //calculate the player's position and throw a projectile but each proejectile have different path and spawn point 
    private IEnumerator ThrowNumberOfExplosiveProjectile(Vector3 targetPosition, int numberOfProjectile)
    {

        for (int i = 0; i < numberOfProjectile; i++)
        {
            var randomOffset = new Vector3(UnityServiceManager.Instance.UnityRandomRange(-20, 20) * 0.1f
                , 0f
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
                , initialControlPointPosition, targetControlPointPosition, GameManager.Instance.GetExplosiveProjectileObject()));

            ProjectileSpawnAngle = ((ProjectileSpawnAngle + 30) % 180);
            yield return new WaitForSeconds(DelayBetweenShoot);

        }
    }
    //Wrapped the corutine code so other Non MonoBehaviour class can call without referencing extra Unity Library
    public void StartThrowNumberOfDirectProjectile(Vector3 targetPosition, int numberOfProjectile)
    {
        StartCoroutine(ThrowNumberOfDirectProjectile(targetPosition, numberOfProjectile));
    }

    //calculate the player's position and throw a projectile but each proejectile have different path and spawn point 
    private IEnumerator ThrowNumberOfDirectProjectile(Vector3 targetPosition, int numberOfProjectile)
    {

        for (int i = 0; i < numberOfProjectile; i++)
        {
            //
            var randomOffset =Quaternion.AngleAxis(UnityServiceManager.Instance.UnityRandomRange(0,180),Vector3.up) * new Vector3(0f
                , 0f
                , DirectProjectileSpawnRadius);

            StartCoroutine(ThrowRandomProjectile(randomOffset + targetPosition, -randomOffset + targetPosition
                , randomOffset + targetPosition, -randomOffset + targetPosition, GameManager.Instance.GetDirectProjectileObject()));


            yield return new WaitForSeconds(DelayBetweenShoot);

        }
    }
    public void StartThrowNumberOfStraightDownProjectile(Vector3 targetPosition, int numberOfProjectile)
    {
        StartCoroutine(ThrowNumberOfStraightDownProjectile(targetPosition, numberOfProjectile));
    }
    //calculate the player's position and spawn projectile and each proejectile go straight down
    private IEnumerator ThrowNumberOfStraightDownProjectile(Vector3 targetPosition, int numberOfProjectile)
    {

        for (int i = 0; i < numberOfProjectile; i++)
        {
            var OffSet = new Vector3(0f, StraightDownProjectailSpawn, 0f);
            var EndOffSet = new Vector3(0f, StraightDownProjectailSpawn - 20, 0f);
            StartCoroutine(ThrowRandomProjectile(OffSet + targetPosition, -EndOffSet + targetPosition
                , OffSet + targetPosition, -EndOffSet + targetPosition, GameManager.Instance.GetStraightDownProjectileObject()));

            yield return new WaitForSeconds(DelayBetweenShoot);
        }
    }
    //throw one projectile using CalculateCubicBezierCurve path
    private IEnumerator ThrowRandomProjectile(Vector3 initialPosition, Vector3 targetPosition, Vector3 intialControlPoint, Vector3 targetControlPoint, GameObject projectileGameObject)
    {
        var projectile = GetInstantiateProjectile(initialPosition, projectileGameObject);

        if (projectile == null)
            yield break;

        ImProjectile projectileClass = projectile.GetComponent<ImProjectile>();
        if (projectileClass == null)
            yield break;

        if (projectileClass is DirectProjectile)
                projectile.transform.LookAt(targetPosition);
        float lt = 0;
        float t = 0;
        float eachTimePoint = 1 / (float)NumOfPostionPoint;
        var lineRenderer = projectile.GetComponent<LineRenderer>();
        var isProjectilePathON = false;
        var realDrawingAmount = (int)(NumOfPostionPoint * 0.01 * PreDrawingAmount);

        DrawSomeAmountOfPathLine(initialPosition, targetPosition, intialControlPoint, targetControlPoint, ref lt, eachTimePoint, lineRenderer, ref isProjectilePathON, realDrawingAmount);

        while (!projectileClass.IsCollideWithOther && t <= 1)
        {
            if (isProjectilePathON && realDrawingAmount <= NumOfPostionPoint)
            {
                lineRenderer.positionCount = realDrawingAmount + 1;
                lineRenderer.SetPosition(realDrawingAmount, CalculateCubicBezierCurve(lt, initialPosition, intialControlPoint, targetControlPoint, targetPosition));
                lt += eachTimePoint;
                realDrawingAmount++;
            }

            yield return new WaitForSeconds(TimeSpeedFactor);
            projectile.transform.position = CalculateCubicBezierCurve(t, initialPosition, intialControlPoint, targetControlPoint, targetPosition);
            t += eachTimePoint;
        }

        if (projectileClass.IsDisposing == false)// if object is still active
            projectileClass.AfterThrow();

    }

    private void DrawSomeAmountOfPathLine(Vector3 initialPosition, Vector3 targetPosition, Vector3 intialControlPoint, Vector3 targetControlPoint, ref float lt, float eachTimePoint, LineRenderer lineRenderer, ref bool isProjectilePathON, int realDrawingAmount)
    {
        if (IsProjectilePathVisible)
        {
            if (lineRenderer != null)
            {
                isProjectilePathON = true;

                for (int i = 0; i < realDrawingAmount; i++)
                {
                    lineRenderer.positionCount = i + 1;
                    lineRenderer.SetPosition(i, CalculateCubicBezierCurve(lt, initialPosition, intialControlPoint, targetControlPoint, targetPosition));
                    lt += eachTimePoint;
                }
            }
        }
    }

    // give random projectile with given initial position
    private GameObject GetInstantiateProjectile(Vector3 initialPosition, GameObject projectileGameObject)
    {
        if (projectileGameObject == null)
            return null;
        projectileGameObject.transform.position = initialPosition;
        projectileGameObject.transform.rotation = Quaternion.identity;
        projectileGameObject.SetActive(true);
        //initialPosition include boss mob position + Some Offset
        return projectileGameObject;
    }
    //draw line before start
    private void PreDrawProjectailPathLine(int preDrawingAmount, Vector3 initialPosition, Vector3 targetPosition, Vector3 intialControlPoint, Vector3 targetControlPoint, GameObject projectile)
    {
        var lineRenderer = projectile.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            for (int i = 0; i < preDrawingAmount; i++)
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
