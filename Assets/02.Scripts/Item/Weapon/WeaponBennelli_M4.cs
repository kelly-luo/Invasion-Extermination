using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBennelli_M4 : MonoBehaviour, ImWeapon
{

    #region ID
    public int EntityID { get; }
    public readonly int instanceID;
    public int InstanceID
    {
        get { return instanceID; }
    }
    #endregion

    #region Bullet
    public int MaxBullet { get; } = 30;
    private int numOfBullet = 0;
    public int NumOfBullet
    {
        get { return numOfBullet; }
        set
        {
            numOfBullet = value;
            if (numOfBullet == 0)
                OnBulletRunOut?.Invoke();
        }
    }
    #endregion

    #region Shake Setting

    public float ShakeDuration { get; } = 0.05f;

    public float ShakeMagnitudePos { get; } = 0.03f;

    public float ShakeMagnitudeRot { get; } = 0.1f;

    private bool isShooting = false;

    public bool IsShooting
    {
        get
        {
            return isShooting;
        }
    }

    #endregion

    public float Damage { get; set; } = 20;

    public float Delay { get; set; }

    public float RequiredScore { get; }

    #region Delegate Event

    public Action OnShotFire { get; set; }
    public Action OnBulletRunOut { get; set; }
    public Action OnReload { get; set; }
    #endregion

    public int LimitStacking { get; }

    public float ReloadTime { get; set; }

    public void Reload(ref int numOfBulletLeft)
    {
        if (numOfBulletLeft <= 0)
            return;

        OnReload?.Invoke();

        if (numOfBulletLeft <= MaxBullet)
        {
            NumOfBullet = numOfBulletLeft;
            numOfBulletLeft = 0;
        }
        else
        {
            numOfBulletLeft -= MaxBullet;
            NumOfBullet = MaxBullet;
        }
    }

    public GameObject Fire(Vector3 playerPosition, Vector3 shootDirection)
    {
        RaycastHit hit;
        // required delay
        OnShotFire?.Invoke();

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 100))
        {

            return hit.transform.gameObject;

            //do the related action with monster here
        }
        else
        {
            return null;
        }

    }
}
