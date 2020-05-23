﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour, IStateController
{
    //Range of detecting the player.
    public float viewRange = 70.0f;

    public float failTraceRange = 300.0f;

    [Range(0, 360)]
    public float viewAngle = 100.0f;

    #region state
    [field: SerializeField]
    public State CurrentState { get; set; }
    //state of default RemainState value (any value but it have to match with the return value of transition ) 
    [field: SerializeField]
    public State RemainState { get; set; }
    #endregion

    #region Animation
    public Animator Animator { get; set; }

    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashCanMove = Animator.StringToHash("CanMove");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashIsHoldingWeapon = Animator.StringToHash("IsHoldingWeapon");
    private readonly int hashReload = Animator.StringToHash("Reload");
    private readonly int hashFire = Animator.StringToHash("Fire");

    #endregion

    #region Weapon
    public bool isHoldingWeapon;

    public GameObject weapon;
    public WeaponM1911 WeaponClass { get; set; }
    #endregion

    public PlayerInformation playerInformation;

    public ObjectStats Stats { get; set; }

    public Transform ObjectTransform { get; set; }

    public Transform PlayerTr { get; set; }
    private Transform weaponHolderTr;

    public ICharacterTranslate MonsterTranslate {get; set;}
    public IUnityServiceManager UnityService { get; set; }


    public List<Transform> WayPoints { get; set; } = new List<Transform>();

    public int NextWayPointIndex { get; set; }
    
    public NavMeshAgent Agent { get; set; }

    private float damping = 1.0f;

    public int PlayerLayer { get; set; }
    public int ObstacleLayer { get; set; }
    public int LayerMask { get; set; }

    private List<GameObject> skins = new List<GameObject>();

    private float lastStateUpdateTime;
    private float stateDelay= 0.6f;

    public float DistancePlayerAndEnemy { get; set; }

    #region Speed Value     
    private readonly float patrolSpeed = 1.5f;

    private readonly float traceSpeed = 2.3f;
    #endregion

    public float Speed
    {
        get { return Agent.velocity.magnitude; }
    }

    private bool patrolling;
    public bool Patrolling
    {
        get { return patrolling; }
        set
        {
            patrolling = value;
            if (patrolling)
            {
                Animator.SetBool(hashCanMove, true);
                Animator.SetFloat(hashSpeed, patrolSpeed);
                Agent.speed = patrolSpeed;

                damping = 1.0f;

                this.MoveWayPoint();
            }
        }
    }

    private Vector3 traceTarget;
    public Vector3 TraceTarget
    {
        get { return traceTarget; }
        set
        {
            traceTarget = value;
            Agent.speed = traceSpeed;
            Animator.SetBool(hashCanMove, true);
            Animator.SetFloat(hashSpeed, traceSpeed);
            damping = 7.0f;
            this.TraceTargetPos(traceTarget);
        }
    }



    void Awake()
    {
        var transforms = GetComponentsInChildren<Transform>();
        foreach (Transform tr in transforms)
        {
            if (tr.gameObject.name == "RWeaponHolder")
                weaponHolderTr = tr;
        }
        this.Stats = new MonsterStats();
        this.Stats.Health = 100f;
        this.ObjectTransform = gameObject.transform;
        this.Animator = GetComponent<Animator>();

        this.playerInformation = GameObject.Find("Player").GetComponent<PlayerInformation>();

    }

    void Start()
    {
        UnityService = UnityServiceManager.Instance;

        InitilizeSkinType();
        InitilizeWaypointGroup();

        Agent = GetComponent<NavMeshAgent>();
        Agent.autoBraking = false;
        Agent.updateRotation = false;
        Agent.speed = patrolSpeed;

        PlayerTr = GameObject.FindGameObjectWithTag("Player").transform;
        
        PlayerLayer = UnityEngine.LayerMask.NameToLayer("Player");
        ObstacleLayer = UnityEngine.LayerMask.NameToLayer("Obstacle");
        LayerMask = 1 << PlayerLayer;
        if (isHoldingWeapon)
        {
            Animator.SetBool(hashIsHoldingWeapon, true);
            EquipWeapon(weapon);
        }
    }


    void Update()
    {
        DistancePlayerAndEnemy = (PlayerTr.position - ObjectTransform.position).sqrMagnitude;
        Debug.Log("DistancePlayerAndEnemy " + DistancePlayerAndEnemy.ToString());
        if (Agent.isStopped == false)
        {
            Quaternion rot = Quaternion.LookRotation(Agent.desiredVelocity);

            ObjectTransform.rotation = Quaternion.Slerp(ObjectTransform.rotation, rot, UnityService.DeltaTime * damping);
        }


        CurrentState.UpdateState(this);


    }

    void LateUpdate()
    {
        this.LookTowardMovingDirection();
        if(patrolling)
            this.UpdateCurrentMovePoint();
    }


    private void InitilizeWaypointGroup()
    {
        var group = GameObject.Find("EnemyWayPoints");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(WayPoints);
            //removing the waypoint folder
            WayPoints.RemoveAt(0);
            NextWayPointIndex = UnityService.Range(0, WayPoints.Count);
        }

    }

    private void SetFireAnimtion()
    {
        Animator.SetTrigger(hashFire);
    }

    private void SetReloadAnimation()
    {

    }

    public void EquipWeapon(GameObject weapon)
    {
        WeaponClass = weapon.GetComponent<WeaponM1911>();
        weapon.transform.parent = weaponHolderTr;
        WeaponClass.OnShotFire += SetFireAnimtion;
        weapon.transform.localScale = new Vector3(180f, 180f, 180f);
        weapon.transform.localRotation = Quaternion.Euler(-194.79f, 57.67899f, -97.009f);
        weapon.transform.localPosition = new Vector3(22.8f, 9.3f, -7.5f);
    }

    private void InitilizeSkinType()
    {
        var objects = gameObject.transform.GetComponentsInChildren<Transform>();

        foreach (Transform oj in objects)
            if (oj.name.StartsWith("Character"))
            {
                oj.gameObject.SetActive(false);
                skins.Add(oj.gameObject);
            }
        int skinTypeIdx = UnityService.Range(0, skins.Count);

        skins[skinTypeIdx].SetActive(true);
    }


    public void TransitionToState(State nextState)
    {
        if (nextState != RemainState)
        {
            CurrentState = nextState;
        }
    }


    private void TraceTargetPos(Vector3 pos)
    {
        if (Agent.isPathStale) return;

        Agent.destination = pos;
        Agent.isStopped = false;
    }

    public void MoveWayPoint()
    {
        if (Agent.isPathStale) return;
        Agent.destination = WayPoints[NextWayPointIndex].position;

        Agent.isStopped = false;
    }

    public void Stop()
    { //Stop All
        Agent.isStopped = true;
        Agent.speed = 0;
        Agent.velocity = Vector3.zero;
        patrolling = false;
    }

    private void LookTowardMovingDirection()
    {
        // if the Agent is point they look toward the moving diection
        if (Agent.isStopped == false)
        {
            Quaternion rot = Quaternion.LookRotation(Agent.desiredVelocity);

            ObjectTransform.rotation = Quaternion.Slerp(ObjectTransform.rotation, rot, UnityService.DeltaTime * damping);
        }
    }

    private void UpdateCurrentMovePoint()
    {
        if (Agent.velocity.sqrMagnitude >= (0.2f * 0.2f) && Agent.remainingDistance <= 0.5f)
        {
            //nextIdx = ++nextIdx % wayPoints.Count; //mod so it can go round
            NextWayPointIndex = UnityService.Range(0, WayPoints.Count);

            //goto next waypoint 
            this.MoveWayPoint();
        }
    }
    
    public void RotateToPlayer()
    {
        Stop();

        Animator.SetBool(hashCanMove, false);
        Animator.SetFloat(hashSpeed, 0f);
        Quaternion lookOnLook = Quaternion.LookRotation(PlayerTr.position - ObjectTransform.position);
        ObjectTransform.rotation =
            Quaternion.Slerp(ObjectTransform.rotation, lookOnLook, UnityService.DeltaTime * damping *10);
  
    }

    public void Attack()
    {
        Agent.isStopped = true;
        Agent.velocity = Vector3.zero;
        patrolling = false;
        Animator.SetBool(hashCanMove, false);
        Animator.SetFloat(hashSpeed, 0f);
        StartCoroutine(AttackCoroutine());
    }
    private IEnumerator AttackCoroutine()
    {
        if (isHoldingWeapon)
        {
            WeaponClass.Fire(ObjectTransform.position + new Vector3(0, 1.5f, 0), ObjectTransform.forward);
        }
        yield return null;
    }

    public void TakeDamage(float Damage)
    {
        Debug.Log($"Monster has taken {Damage}");
        Stats.Health -= Damage;
        if (Stats.Health <= 0)
            Debug.Log("Monster has died.");
            this.OnDeath();
    }

    public void OnDeath()
    {
        //player gains points
        this.playerInformation.Score += 10;

        isHoldingWeapon = false;
        Animator.SetBool(hashIsHoldingWeapon, false);
        this.gameObject.tag = "Untagged";
        Animator.SetInteger(hashDieIdx, UnityService.Range(0, 3));
        Animator.SetTrigger(hashDie);
        GetComponent<CapsuleCollider>().enabled = false;

        //Agent.isStopped = true;
        Agent.velocity = Vector3.zero;
        patrolling = false;
        var script = GetComponent<MonsterController>();
        script.enabled = false;

    }


}
