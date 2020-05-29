using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAK74 : MonoBehaviour, ImWeapon
{

    #region ID

    public int EntityID { get; } = 0;
    public readonly int instanceID;
    public int InstanceID
    {
        get { return instanceID; }
    }
    #endregion

    #region Bullet
    public int MaxBullet { get; } = 30;
    private int numOfBullet = 30;
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

    #endregion
    
    #region Shooting Setting 

    public float Damage { get; set; } = 20;

    public float Delay { get; set; } = 0.2f;

    public float RequiredScore { get; }

    private bool isShooting = false;

    private float lastShootTime = 0f;

    #region LayerMask
    private int enemyLayer;
    private int obstacleLayer;
    private int layerMask;
    #endregion

    #region Gizmos
    private Vector3 playerPositions;
    private Vector3 shootDirections;
    #endregion

    public bool IsShooting
    {
        get
        {
            return isShooting;
        }
    }

    #endregion

    #region Delegate Event

    public Action OnShotFire { get; set; }
    public Action OnBulletRunOut { get; set; }
    public Action OnReload { get; set; }
    #endregion

    #region Audio
    private AudioSource audio;

    public AudioClip fireSfx;
    #endregion

    public int LimitStacking { get; }

    public float ReloadTime { get; set; }

    public IUnityServiceManager UnityService { get; set; } = new UnityServiceManager();



    void Start()
    {
        audio = GetComponent<AudioSource>();

        this.SetLayer();
    }

    public void SetLayer()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        layerMask = (1 << enemyLayer) | (1 << obstacleLayer);
    }

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

    public GameObject Fire(Vector3 playerPosition,Vector3 shootDirection)
    {
        playerPositions = playerPosition;
        shootDirections = shootDirection;
        if (lastShootTime + Delay > UnityService.TimeAtFrame)
            return null;

        isShooting = true;
        lastShootTime = UnityService.TimeAtFrame;

        if (NumOfBullet > 0)
        {
            OnShotFire?.Invoke();
            if (audio != null)
                audio.PlayOneShot(fireSfx, 0.3f);
        }

        RaycastHit hit;
        if (Physics.Raycast(playerPosition, shootDirection, out hit, 70, layerMask))
        {
            var hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag(("Enemy")) || hitObject.CompareTag(("Human")))
            {
                var control = hitObject.GetComponent<MonsterController>();
                control.TakeDamage(Damage);
                isShooting = false;
            }


            return hitObject;

            //do the related action with monster here
        }
        else
        {
            return null;
        }
    }


    void OnDrawGizmos( )
    {
        Gizmos.DrawLine(playerPositions, playerPositions +(70 * shootDirections));
    }
}
