using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponM4_8 : MonoBehaviour, ImWeapon
{

    #region ID

    public int EntityID { get; set; } = 2;
    private int instanceID;
    public int InstanceID
    {
        get { return instanceID; }
        set { instanceID = value; }
    }
    public int StackAmount { get; set; } = 1;
    #endregion

    #region Bullet
    public int MaxBullet { get; } = 2;
    private int numOfBullet = 2;
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

    public float ShakeDuration { get; } = 0.08f;

    public float ShakeMagnitudePos { get; } = 0.07f;

    public float ShakeMagnitudeRot { get; } = 0.1f;

    #endregion

    #region Shooting Setting 
    public float ShootKnockbackVector { get; set; } = 5f;

    public float Damage { get; set; } = 100;

    public float Delay { get; set; } = 0.7f;

    public float RequiredScore { get; }

    private bool isShooting = false;

    private float lastShootTime = 0f;

    public float FiringRange { get; set; } = 200f;

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

    public AudioClip emptySfx;

    public float SoundVolume { get; set; } = 0.2f;
    #endregion

    public int StackLimit { get; }

    public float ReloadDuration { get; set; }

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

    public void Reload(ref int ammoLeft)
    {
        if (ammoLeft <= 0)
            return;

        OnReload?.Invoke();

        if (ammoLeft <= MaxBullet)
        {
            NumOfBullet = ammoLeft;
            ammoLeft = 0;
        }
        else
        {
            ammoLeft -= MaxBullet;
            NumOfBullet = MaxBullet;
        }
    }

    public GameObject Fire(Vector3 playerPosition, Vector3 shootDirection)
    {
        playerPositions = playerPosition;
        shootDirections = shootDirection;
        if (lastShootTime + Delay > UnityService.TimeAtFrame)
            return null;

        if (NumOfBullet <= 0)
        {
            if (audio != null)
                audio.PlayOneShot(emptySfx, SoundVolume);

            lastShootTime = UnityService.TimeAtFrame;
            return null;
        }

        isShooting = true;
        lastShootTime = UnityService.TimeAtFrame;

        OnShotFire?.Invoke();
        if (audio != null)
            audio.PlayOneShot(fireSfx, SoundVolume);

        NumOfBullet--;
        RaycastHit hit;
        if (Physics.Raycast(playerPosition, shootDirection, out hit, FiringRange, layerMask))
        {
            var hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag(("Enemy")) || hitObject.CompareTag(("Human")))
            {
                var control = hitObject.GetComponent<MonsterController>();
                control.TakeDamage(Damage);
                hitObject.GetComponent<Rigidbody>().AddForce(shootDirection * ShakeMagnitudePos * ShootKnockbackVector, ForceMode.Impulse);
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


    void OnDrawGizmos()
    {
        Gizmos.DrawLine(playerPositions, playerPositions + (FiringRange * shootDirections));
    }
}
