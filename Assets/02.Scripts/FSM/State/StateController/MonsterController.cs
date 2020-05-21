using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour, IStateController
{
    //Range of detecting the player.
    public float viewRange = 30.0f;

    public float failTraceRange = 100.0f;

    [Range(0, 360)]
    public float viewAngle = 120.0f;

    #region state
    [field: SerializeField]
    public State CurrentState { get; set; }
    //state of default RemainState value (any value but it have to match with the return value of transition ) 
    [field: SerializeField]
    public State RemainState { get; set; }
    #endregion
    #region Animation
    public Animator Animator { get; set; }
    #endregion

    public ObjectStats Stats { get; set; }

    public Transform ObjectTransform { get; set; }

    public Transform PlayerTr { get; set; }

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

    #region Speed Value     
    private readonly float patrolSpeed = 20.5f;

    private readonly float traceSpeed = 2.8f;
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

            damping = 7.0f;
            this.TraceTargetPos(traceTarget);
        }
    }


    void Awake()
    {
        this.Stats = new MonsterStats();
        this.ObjectTransform = gameObject.transform;
        this.Animator = GetComponent<Animator>();

        this.MonsterTranslate = new MonsterTranslate(ObjectTransform);

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
            Debug.Log(WayPoints[1].position.ToString());
        }
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

    // Update is called once per frame
    void Update()
    {
        this.LookTowardMovingDirection();
        this.UpdateCurrentMovePoint();
        CurrentState.UpdateState(this);
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
        Agent.velocity = Vector3.zero;
        patrolling = false;
    }

    private void LookTowardMovingDirection()
    {
        // if the Agent is point they look toward the moving diection
        if (Agent.isStopped == false)
        {
            Quaternion rot = Quaternion.LookRotation(Agent.desiredVelocity);

            ObjectTransform.rotation = Quaternion.Slerp(ObjectTransform.rotation, rot, Time.deltaTime * damping);
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

    public void TakeDamage(float Damage)
    {
        Stats.HP -= Damage;
    }

    public void OnDeath()
    {

    }


}
