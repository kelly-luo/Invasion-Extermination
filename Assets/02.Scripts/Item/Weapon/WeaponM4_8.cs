using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponM4_8 : MonoBehaviour, ImWeapon
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

    public GameObject Fire()
    {
        // required delay
        OnShotFire?.Invoke();

        RaycastHit hit;
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
