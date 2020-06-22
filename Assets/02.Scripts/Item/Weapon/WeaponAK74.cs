using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAK74 : MonoBehaviour, ImWeapon
{

    #region ID

    public int EntityID { get; set; } = 0;
    private int instanceID;
    public int InstanceID
    {
        get { return instanceID; }
        set { instanceID = value; }
    }
    public int StackAmount { get; set; } = 1;
    #endregion

    #region Bullet
    public int MaxBullet { get; set; } = 30;
    private int numOfBullet = 30;
    public int NumOfBullet
    {
        get { return numOfBullet; }
        set
        {
            numOfBullet = value;
            if (numOfBullet <= 0)
            {
                numOfBullet = 0;
                OnBulletRunOut?.Invoke();
            }
        }
    }
    #endregion

    #region Shake Setting

    public float ShakeDuration { get; } = 0.05f;

    public float ShakeMagnitudePos { get; } = 0.03f;

    public float ShakeMagnitudeRot { get; } = 0.1f;

    #endregion

    #region Shooting Setting 
    public float ShootKnockbackVector { get; set; } = 1000f;

    public float Damage { get; set; } = 20;

    public float Delay { get; set; } = 0.2f;

    public float RequiredScore { get; }

    private bool isShooting = false;

    private float lastShootTime = 0f;

    public float FiringRange { get; set; } = 70;

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

    public float ReloadDuration { get; set; } = 1.2f;

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
        //reload anime
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

    public GameObject Fire(Vector3 playerPosition,Vector3 shootDirection)
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
            BulletHitEffect(hit);
            if (hitObject.CompareTag(("Enemy")) || hitObject.CompareTag(("Human")))
            {
                var control = hitObject.GetComponent<MonsterController>();
                control.TakeDamage(Damage);
                hitObject.GetComponent<Rigidbody>().AddForce(shootDirection * ShakeMagnitudePos * ShootKnockbackVector, ForceMode.Impulse);
                //get Rotation and position of hit point
               
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

    private void BulletHitEffect(RaycastHit hit)
    {
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
        if(GameManager.Instance != null)
            GameManager.Instance.SpawnBulletHitObject(hit.point, rot);
    }

    void OnDrawGizmos( )
    {
        Gizmos.DrawLine(playerPositions, playerPositions +(FiringRange * shootDirections));
    }
}
