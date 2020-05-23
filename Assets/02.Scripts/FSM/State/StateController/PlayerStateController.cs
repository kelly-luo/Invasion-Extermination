using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class PlayerStateController : MonoBehaviour, IStateController
{
    #region state
    [field: SerializeField]
    public State CurrentState { get; set; }
    //state of default RemainState value (any value but it have to match with the return value of transition ) 

    [field: SerializeField]
    public State RemainState { get; set; }

    public ObjectStats Stats { get; set; } // Kelly: have to rethink about this

    #endregion state

    #region Unity Component
    [field: SerializeField]
    public Transform ObjectTransform { get; set; }
    public PlayerInformation playerStats;

    #endregion

    #region Camera Setting
    [field: SerializeField]
    public GameObject CameraRig { get; set; }
    public Transform CameraRigTr { get; set; }

    public Transform CameraTr { get; set; }
    public ICameraControl CameraCtrl { get; set; }
    #endregion 

    #region Animation
    public Animator Animator { get; set; }

    private readonly int hashXDirectionSpeed = Animator.StringToHash("XDirectionSpeed");
    private readonly int hashZDirectionSpeed = Animator.StringToHash("ZDirectionSpeed");
    private readonly int hashFire = Animator.StringToHash("Fire");

    private readonly int hashSpeed = Animator.StringToHash("Speed");

    private readonly int hashIsRunning = Animator.StringToHash("IsRunning");
    private readonly int hashIsHoldingRifle = Animator.StringToHash("IsHoldingRifle");
    private bool isRunning = false;
    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        set
        {
            if (isRunning != value)
            {
                PlayerTranslate.IsRunning = value;
                Animator.SetBool(hashIsRunning, value);
                isRunning = value;
            }
            if (value)
            {
                Animator.SetFloat(hashSpeed, 3.0f);
            }
            else
            {
                Animator.SetFloat(hashSpeed, 1.5f);
            }
        }
    }

    private readonly int hashIsSitting = Animator.StringToHash("IsSitting");
    private bool isSitting = false;
    public bool IsSitting
    {
        get
        {
            return isSitting;
        }
        set
        {
            if (isSitting != value)
            {
                PlayerTranslate.IsSitting = value;
                Animator.SetBool(hashIsSitting, value);
                isSitting = value;
            }
        }
    }
    #endregion

    #region Character Avatar Value
    private Quaternion headRot = Quaternion.Euler(0f, 0, 0f);
    private Quaternion actualHeadRot;

    public float MaxAngleOnHeadRotation { get; set; } = 90f;
    public float MinAngleOnHeadRotation { get; set; } = -90f;

    private Transform neckTr;
    private Transform headTr;

    #endregion

    #region Input Values
    public float Vertical { get; set; }
    public float Horizontal { get; set; }

    public bool HasWeapon { get; set; }

    private bool isHoldingRifle = false;
    public bool IsHoldingRifle
    {
        get { return isHoldingRifle; }
        set
        {           
            if (HasWeapon)
            {
                Animator.SetBool(hashIsHoldingRifle, value);
                isHoldingRifle = value;
                this.UpdateTheTexturWhenCameraViewChange();
            }

        }
    }
    #endregion

    #region Weapon Setting

    public GameObject weapon;
    private Transform weaponHolderTr;

    #endregion

    // state will change depends on this class (speed) 
    public ICharacterTranslate PlayerTranslate { get; set; }
    public IUnityServiceManager UnityService { get; set; } = UnityServiceManager.Instance;
    public IWeaponManager WeaponManager { get; set; }

    #region MonoBehaviour Base Function
    void Awake()
    {
        SetBoneTransform();

        //Stats = new PlayerStats();
        this.ObjectTransform = GetComponent<Transform>();
        this.Animator = GetComponent<Animator>();
        this.WeaponManager = gameObject.AddComponent<PlayerWeaponManager>();
        this.playerStats = GetComponent<PlayerInformation>();
        playerStats.Health = 100;

        PlayerTranslate = new PlayerTranslate(ObjectTransform);
        actualHeadRot = headTr.localRotation;

    }

    private void SetBoneTransform()
    {
        var transforms = GetComponentsInChildren<Transform>();
        foreach (Transform tr in transforms)
        {
            if (tr.gameObject.name == "Neck")
                neckTr = tr;
            if (tr.gameObject.name == "Head")
                headTr = tr;
            if (tr.gameObject.name == "RWeaponHolder")
                weaponHolderTr = tr;
        }
    }

    void Start()
    {
        CameraCtrl = CameraRig.GetComponent<CameraControl>();
        CameraRigTr = CameraRig.GetComponent<Transform>();
        CameraTr = CameraCtrl.GetCameraTransform();


        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;


        this.UpdateTheTexturWhenCameraViewChange();

        CameraCtrl.OnViewChange += UpdateTheTexturWhenCameraViewChange;
        this.EquipWeapon(weapon);

    }

    void Update()
    {
        //update scene

        this.UpdateUserInput();
        this.CurrentState.UpdateState(this);

        if (UnityService.GetMouseButtonUp(0))
        {
            this.Attack();
        }
    }


    void LateUpdate()
    {
        if (IsHoldingRifle)
        {
            this.WeaponManager.UpdateFirstPersonViewWeaponPosition();
        }
        this.RotateNeck();
        this.RotateHeadAndAvatar(CameraCtrl.YRot);
    }
    #endregion

    public void EquipWeapon(GameObject Weapon)
    {
        this.HasWeapon = true;
        this.WeaponManager.EquipNewWeapon(Weapon, CameraTr, weaponHolderTr);
        this.WeaponManager.AddOnShootFireEvent(ShakeCameraWhenShoot);
        this.WeaponManager.AddOnShootFireEvent(SetWeaponFireAnimation);
        this.UpdateTheTexturWhenCameraViewChange();

    }


    #region animation method
    public void MoveAnimation(float xSpeed, float zSpeed)
    {
        this.WeaponManager.UpdateWeaponBob(xSpeed, zSpeed);

        if (!isRunning && zSpeed > 0.5f)
        {
            zSpeed = 0.5f;
        }
        if(zSpeed <= 0f )
        {
            IsRunning = false;
        }

        this.Animator.SetFloat(hashXDirectionSpeed, xSpeed);
        this.Animator.SetFloat(hashZDirectionSpeed, zSpeed);


        if (((xSpeed > 0) || (xSpeed < 0) || (zSpeed > 0) || (zSpeed < 0)) || IsHoldingRifle)
            this.RotateAvatarTowardSight();// minus the amount of the value of angle to the head to lotate the body while moving the body .
    }

    #endregion

    #region CharacterBodyMove

    private void RotateNeck()
    {
        //you might be wondering why did i put the x on z axis 
        //it is because the I found that local Transform of the Neck bone in model was reversed. 
        //neckTr.localRotation = Quaternion.Euler(0f, 0f, CameraTr.localRotation.eulerAngles.x);

    }

    private void RotateHeadAndAvatar(float yRot)
    {
        // this part because animation changes the head position in Update so i have to
        // save the rotation of head on actualHeadRot value and use it as head rotation.
        headTr.localRotation = actualHeadRot;
        //NextAngle is amount of angle change in head rotation
        var nextAngle = headRot.eulerAngles.y + yRot;
        if (nextAngle < 180)
        {
            //so it is necessary to seperate the amount of angle that go in to the head rotation 
            //and boby rotation(if the angle exceed maxAngle of Head Rotation)
            if (nextAngle > MaxAngleOnHeadRotation)
            {
                var leftoverAngle = (nextAngle - MaxAngleOnHeadRotation);
                var angleUpToLimit = yRot - leftoverAngle;

                headRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                headTr.localRotation *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                actualHeadRot = headTr.localRotation;

                ObjectTransform.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0f, yRot, 0f);
                headTr.localRotation *= Quaternion.Euler(0f, yRot, 0f);
                actualHeadRot = headTr.localRotation;

            }
        }
        else if (nextAngle > 180)
        {
            if (nextAngle < (360 + MinAngleOnHeadRotation))
            {
                var leftoverAngle = (nextAngle - (360 + MinAngleOnHeadRotation));
                var angleUpToLimit = yRot - leftoverAngle;
                headRot *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                headTr.localRotation *= Quaternion.Euler(0f, angleUpToLimit, 0f);
                actualHeadRot = headTr.localRotation;

                ObjectTransform.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

            }
            else
            {
                headRot *= Quaternion.Euler(0, yRot, 0);
                headTr.localRotation *= Quaternion.Euler(0f, yRot, 0f);
                actualHeadRot = headTr.localRotation;
            }
        }
    }

    private void RotateAvatarTowardSight()
    {
        var rotationSpeed = 150f;
        var bodyRot = ObjectTransform.localRotation;
        var lookRotation = Quaternion.LookRotation(CameraRigTr.forward);

        float angle = Quaternion.Angle(transform.rotation, lookRotation);
        float timeToComplete = angle / rotationSpeed;
        float donePercentage = Mathf.Min(1F, UnityService.DeltaTime / timeToComplete);

        ObjectTransform.localRotation = Quaternion.Slerp(ObjectTransform.localRotation, lookRotation, donePercentage);

        //take the amount of change in body rotation 
        var ChangeRotBody = ObjectTransform.localRotation.eulerAngles.y - bodyRot.eulerAngles.y;
        //add to Head Rotation
        headRot *= Quaternion.Euler(0f, -ChangeRotBody, 0f);
        actualHeadRot *= Quaternion.Euler(0f, -ChangeRotBody, 0f);
    }

    #endregion

    #region callbackWhenShootMotion

    public void SetWeaponFireAnimation()
    {
        if (HasWeapon)
            Animator.SetTrigger(hashFire);
    }

    public void ShakeCameraWhenShoot()
    {
        StartCoroutine(CameraCtrl.ShakeCamera(WeaponManager.FirstWeaponClass.ShakeDuration, WeaponManager.FirstWeaponClass.ShakeMagnitudePos, WeaponManager.FirstWeaponClass.ShakeMagnitudeRot));
    }

    #endregion

    public void TransitionToState(State nextState)
    {
        if (nextState != RemainState)
        {
            CurrentState = nextState;
        }
    }

    public void Attack()
    {
        if(IsHoldingRifle)
        {
            StartCoroutine(WeaponManager.Attack(CameraTr.transform.position, CameraTr.transform.forward));
        }
    }
    private void UpdateUserInput()
    {
        if (UnityService.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = !isRunning;
        }
        if (UnityService.GetKeyUp(KeyCode.LeftControl))
        {
            IsRunning = !isSitting;
        }
        if (UnityService.GetKeyUp(KeyCode.Z))
        {
            if(HasWeapon)
                IsHoldingRifle = !IsHoldingRifle;
        }
    }

    //weapon
    public void UpdateTheTexturWhenCameraViewChange()
    {
        this.UpdateCharacterTexture();
        this.UpdateWeaponTexture();
    }

    private void UpdateCharacterTexture()
    {
        var currentViewMode = CameraCtrl.CurrentViewMode;
        var renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

        if (currentViewMode == CameraViewType.First)
        {
            renderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }
        else if (currentViewMode == CameraViewType.Third)
        {
            renderer.shadowCastingMode = ShadowCastingMode.On;
        }
    }

    private void UpdateWeaponTexture()
    {
        var currentViewMode = CameraCtrl.CurrentViewMode;
        if (currentViewMode == CameraViewType.First)
        {
            if (HasWeapon)
            {
                if (IsHoldingRifle)
                {

                    WeaponManager.SetFirstPersonWeaponVisible(true);
                    WeaponManager.SetThirdPersonWeaponVisible(false);
                }
                else
                {
                    WeaponManager.SetFirstPersonWeaponVisible(false);
                    WeaponManager.SetThirdPersonWeaponVisible(false);
                }
            }
        }
        else if (currentViewMode == CameraViewType.Third)
        {
            if (HasWeapon)
            {
                if (IsHoldingRifle)
                {
                    WeaponManager.SetFirstPersonWeaponVisible(false);
                    WeaponManager.SetThirdPersonWeaponVisible(true);
                }
                else
                {
                    WeaponManager.SetFirstPersonWeaponVisible(false);
                    WeaponManager.SetThirdPersonWeaponVisible(false);
                }
            }
        }
    }

    public void UnEquipWeapon()
    {
        IsHoldingRifle = false;
        HasWeapon = false;
        WeaponManager.SetFirstPersonWeaponActive(false);
        WeaponManager.SetThirdPersonWeaponActive(false);
    }

    public void TakeDamage(float Damage)
    {
        Debug.Log($"Player has taken {Damage}");
        playerStats.Health -= Damage;
        if (playerStats.Health <= 0)
            Debug.Log("Player died. Respawning now.");
            this.OnDeath();
    }

    public void OnDeath()
    {

    }
}





