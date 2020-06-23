using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour, IStateController
{
    public GameObject healthBarObject;
    private EnemyHealthBar healthbar;

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
    private readonly int hashTakeHit = Animator.StringToHash("TakeHit");
    private readonly int hashThrowAttackFullOne = Animator.StringToHash("ThrowAttackFullOne");
    private readonly int hashThrowAttackFullTwo = Animator.StringToHash("ThrowAttackFullTwo");
    private readonly int hashThrowDirectAttack = Animator.StringToHash("ThrowDirectAttack");
    private readonly int hashThrowAttack = Animator.StringToHash("ThrowAttack");

    public bool isBoss = false;


    #endregion

    #region Weapon
    public bool isHoldingWeapon;

    public GameObject weapon;
    public WeaponM1911 WeaponClass { get; set; }

    private Transform weaponHolderTr;
    #endregion

    #region Character Information
    public ObjectStats Stats { get; set; }
    public Transform ObjectTransform { get; set; }
    public float SelfDestroyDelay { get; set; } = 40f;
    #endregion

    #region Target Information
    public PlayerInformation playerInformation;
    public Transform PlayerTr { get; set; }
    #endregion

    #region layermask
    public int PlayerLayer { get; set; }
    public int ObstacleLayer { get; set; }
    public int LayerMask { get; set; }
    #endregion

    #region PatrolWayPoints
    public List<Transform> WayPoints { get; set; } = new List<Transform>();
    public int NextWayPointIndex { get; set; }

    #endregion

    #region Speed Value     
    private readonly float patrolSpeed = 1.5f;

    private readonly float traceSpeed = 2.3f;

    public float Speed
    {
        get { return Agent.velocity.magnitude; }
    }
    #endregion

    #region projectile
    public ProjectileManager ProjectileManager { get; set; }
    #endregion

    #region State Value properties
    public float DistancePlayerAndEnemy { get; set; }

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
    #endregion

    public float ItemSelfDestoryDelay { get; set; } = 30f;

    public float StateUpdateDelayTime { get; set; } = 0.075f;
    private float currentTime = 0f;


    public NavMeshAgent Agent { get; set; }

    public bool isEnabled;

    private bool isAgentEnabled;
    public bool IsAgentEnabled
    {
        get { return isAgentEnabled; }
        set
        {
            if (value)
            {
                InitilizeNavAgent();
                isAgentEnabled = value;
            }
        }
    }

    private float damping = 1.0f;

    private List<GameObject> skins = new List<GameObject>();

    public IUnityServiceManager UnityService { get; set; }

    #region MonoBehaviour Base Function
    void Awake()
    {
        ProjectileManager = GetComponentInChildren<ProjectileManager>();
        var transforms = GetComponentsInChildren<Transform>();
        foreach (Transform tr in transforms)
        {
            if (tr.gameObject.name == "RWeaponHolder")
                weaponHolderTr = tr;
        }
        MonsterStats mStats = new MonsterStats();
        if (isBoss)
        {
            mStats.Health = 2000f;
            mStats.maxHealth = mStats.Health;
        }
        else
        {
            mStats.Health = 100f;
            mStats.maxHealth = mStats.Health;
        }

        this.Stats = mStats;

        this.ObjectTransform = gameObject.transform;
        this.Animator = GetComponent<Animator>();

        this.playerInformation = GameObject.Find("Player").GetComponent<PlayerInformation>();

        if (isEnabled)
            IsAgentEnabled = true;
    }

    void Start()
    {
        healthbar = this.gameObject.AddComponent<EnemyHealthBar>();
        healthbar.hpBarPrefab = healthBarObject;
        healthbar.SetHealthBar();

        UnityService = UnityServiceManager.Instance;

        InitilizeSkinType();
        InitilizeWaypointGroup();
        InitilizeLayerMask();

        if (isHoldingWeapon)
        {
            Animator.SetBool(hashIsHoldingWeapon, true);
            EquipWeapon(weapon);
        }

    }


    void FixedUpdate()
    {
        DistancePlayerAndEnemy = (PlayerTr.position - ObjectTransform.position).sqrMagnitude;

        if (isAgentEnabled)
        {
            
            //timer
            if (currentTime + StateUpdateDelayTime < UnityService.TimeAtFrame)
            {
                CurrentState.UpdateState(this);
                currentTime = UnityService.TimeAtFrame;
            }

        }
    }

    void LateUpdate()
    {
        if(isAgentEnabled) this.LookTowardMovingDirection();
        if (patrolling) this.UpdateCurrentMovePoint();

    }

    #endregion

    #region Initiallize Method
    private void InitilizeLayerMask()
    {
        PlayerTr = GameObject.FindGameObjectWithTag("Player").transform;

        PlayerLayer = UnityEngine.LayerMask.NameToLayer("Player");
        ObstacleLayer = UnityEngine.LayerMask.NameToLayer("Obstacle");

        LayerMask = 1 << PlayerLayer;
    }

    private void InitilizeNavAgent()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.isStopped = false;
        Agent.autoBraking = false;
        Agent.updateRotation = false;
        Agent.speed = patrolSpeed;
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
    #endregion

    #region Animation
    private void SetFireAnimtion()
    {
        Animator.SetTrigger(hashFire);
    }

    private void SetReloadAnimation()
    {

    }
    public void TriggerTakeHit()
    {
        Animator.SetTrigger(hashTakeHit);
    }
    public void TriggerThrowAttackFullOne()
    {
        StopAgent();
        Animator.SetBool(hashCanMove, false);
        Animator.SetFloat(hashSpeed, 0f);
        Animator.SetTrigger(hashThrowAttackFullOne);
    }
    public void TriggerThrowAttackFullTwo()
    {
        StopAgent();
        Animator.SetBool(hashCanMove, false);
        Animator.SetFloat(hashSpeed, 0f);
        Animator.SetTrigger(hashThrowAttackFullTwo);
    }
    public void TriggerThrowDirectAttack()
    {
        StopAgent();
        Animator.SetBool(hashCanMove, false);
        Animator.SetFloat(hashSpeed, 0f);
        Animator.SetTrigger(hashThrowDirectAttack);
    }
    public void TriggerThrowAttack()
    {
        StopAgent();
        Animator.SetBool(hashCanMove, false);
        Animator.SetFloat(hashSpeed, 0f);
        Animator.SetTrigger(hashThrowAttack);
    }

    #endregion



    #region State Related Method
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
    #endregion

    #region Movement Related Method


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
        StopAgent();

        Animator.SetBool(hashCanMove, false);
        Animator.SetFloat(hashSpeed, 0f);
        Quaternion lookOnLook = Quaternion.LookRotation(PlayerTr.position - ObjectTransform.position);
        ObjectTransform.rotation =
            Quaternion.Slerp(ObjectTransform.rotation, lookOnLook, UnityService.DeltaTime * damping * 10);

    }
    #endregion
 
    public void StopAgent()
    { //Stop All
        Agent.isStopped = true;
        Agent.speed = 0;
        Agent.velocity = Vector3.zero;
        patrolling = false;
    }

    public void EquipWeapon(GameObject weapon)
    {
        //Set Weapon Class
        WeaponClass = weapon.GetComponent<WeaponM1911>();
        //Set(or Subscribe) call back method(or event) on fire happen
        WeaponClass.OnShotFire += SetFireAnimtion;
        SetWeaponPosition(weapon);

    }
    private void SetWeaponPosition(GameObject weapon)
    {
        weapon.transform.parent = weaponHolderTr;
        weapon.transform.localScale = new Vector3(180f, 180f, 180f);
        weapon.transform.localRotation = Quaternion.Euler(-194.79f, 57.67899f, -97.009f);
        weapon.transform.localPosition = new Vector3(22.8f, 9.3f, -7.5f);
    }

    public void Attack()
    {
        StopAgent();
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
        if(isBoss)
        {
            Debug.Log($"Boss Health Left: {Stats.Health.ToString()}");
            this.TriggerTakeHit();
        }
        Debug.Log($"{this.gameObject.tag} has taken {Damage}");
        Stats.Health -= Damage;
        healthbar.onDamage(Stats);
        if (Stats.Health <= 0)
        {
            Debug.Log($"{this.gameObject.tag} has died.");
            this.OnDeath();
        }

    }

    public void OnDeath()
    {
        if(this.gameObject.tag == "Enemy")
        {
            //player killed a monster and gains 10 points
            this.playerInformation.Score += 10;
        }
        else if(this.gameObject.tag == "Human")
        {
            //player killed a human and 20 points deducted
            this.playerInformation.Score -= 20;
        }

        if(isAgentEnabled) StopAgent();

        TurnOffWeaponAnimation();

        this.gameObject.tag = "Untagged";

        Animator.SetInteger(hashDieIdx, UnityService.Range(0, 3));
        Animator.SetTrigger(hashDie);

        GetComponent<CapsuleCollider>().enabled = false;

        LootMoneyPopUp();
        LootAmmoPopUp();
        LootHealthPopUp();

        
        if (isBoss)
        {
            StartCoroutine(this.timerWait());
        }


        Destroy(gameObject,SelfDestroyDelay);

        var script = GetComponent<MonsterController>();
        script.enabled = false;

    }

    //
    // timerWait()
    // ~~~~~~~~~~~
    // Waits for 7 Seconds for player to get Boss loot
    //
    // returns      IEnumerator for Coroutine
    //
    private IEnumerator timerWait()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    private void LootMoneyPopUp()
    {

        //Spwan itemPopUpEffect
        GameManager.Instance.SpawnItemPopUpEffectObject(gameObject.transform.position, Quaternion.identity);

        var numberOfBill = UnityService.UnityRandomRange(1, 10);

        for (int i = 0; i < numberOfBill; i++)
        {
            var moneyBillObject = GameManager.Instance.GetMoneyBillObject();
            if (moneyBillObject != null)
            {
                moneyBillObject.GetComponent<Money>().MoneyAmount = UnityService.UnityRandomRange(10, 44);
                moneyBillObject.transform.position = transform.position;
                moneyBillObject.transform.rotation = transform.rotation;
                moneyBillObject.SetActive(true);
                this.StartCoroutine(SelfDeActiveWithDelay(moneyBillObject, ItemSelfDestoryDelay));
            }
        }

    }
    private void LootAmmoPopUp()
    {
        var numberOfAmmo = UnityService.UnityRandomRange(1, 5);

        for (int i = 0; i < numberOfAmmo; i++)
        {
            var ammoObject = GameManager.Instance.GetAmmoObject();
            if (ammoObject != null)
            {
                ammoObject.GetComponent<Ammo>().AmmoAmount = UnityService.UnityRandomRange(10, 20);
                ammoObject.transform.position = transform.position;
                ammoObject.transform.rotation = transform.rotation;
                ammoObject.SetActive(true);
                this.StartCoroutine(SelfDeActiveWithDelay(ammoObject, ItemSelfDestoryDelay));
            }
        }
    }

    private void LootHealthPopUp()
    {
        var numberOfHealth = UnityService.UnityRandomRange(0, 2);

        for (int i = 0; i < numberOfHealth; i++)
        {
            var healthObject = GameManager.Instance.GetHealthObject();
            if (healthObject != null)
            {
                healthObject.GetComponent<HealthDrop>().HealthAmount = UnityService.UnityRandomRange(20, 30);
                healthObject.transform.position = transform.position;
                healthObject.transform.rotation = transform.rotation;
                healthObject.SetActive(true);
                this.StartCoroutine(SelfDeActiveWithDelay(healthObject, ItemSelfDestoryDelay));
            }
        }
    }
   
    private IEnumerator SelfDeActiveWithDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }

    private void TurnOffWeaponAnimation()
    {
        isHoldingWeapon = false;
        Animator.SetBool(hashIsHoldingWeapon, false);
    }




}
