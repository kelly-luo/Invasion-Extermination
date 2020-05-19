using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PlayerStateController : MonoBehaviour, IStateController
{
    #region state
    [field: SerializeField]
    public State CurrentState { get; set; }
    //state of default RemainState value (any value but it have to match with the return value of transition ) 

    [field: SerializeField]
    public State RemainState { get; set; }

    public ObjectStats Stats { get; set; }
    #endregion state

    #region Unity Component
    [field: SerializeField]
    public Transform PlayerTransform { get; set; }

    #endregion


    #region Camera Setting
    [field: SerializeField]
    public GameObject CameraRig { get; set; }
    public Transform CameraRigTr { get; set; }

    public Transform CameraTr { get; set; }
    public ICameraControl CameraCtrl{ get;set;}


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
            if(value)
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
    private bool isHoldingRifle;
    public bool IsHoldingRifle
    {
        get { return isHoldingRifle; }
        set
        {
            Animator.SetBool(hashIsHoldingRifle, value);

            thirdPersonViewWeapon.SetActive(value);       

            isHoldingRifle = value;
        }
    }
    #endregion

    #region Weapon Setting
    public GameObject fisrtPersonViewWeapon;

    public ImWeapon weaponClass;

    public float weaponOffX = 0.5f;
    public float weaponOffY = 0.5f;
    public float weaponOffZ = 0.5f;

    public Vector3 weaponBob;
    public float BobFrequency { get; set; } = 10f;
    public float BobAmount { get; set; } = 0.05f;
    
    public Vector3 weaponRebound;


    private GameObject thirdPersonViewWeapon;
    private Transform weaponHolderTr;

    #endregion

    public float StateTimeElapsed { get; set; }

    // state will change depends on this class (speed) 
    public ICharacterTranslate PlayerTranslate { get; set; }
    public IUnityServiceManager UnityService { get; set; }

    #region MonoBehaviour Base Function
    void Awake()
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


        PlayerTransform = GetComponent<Transform>();
        Animator = GetComponent<Animator>();
        PlayerTranslate = new PlayerTranslate(PlayerTransform);
        actualHeadRot = headTr.localRotation;

    }

    void Start()
    {
        CameraCtrl = CameraRig.GetComponent<CameraControl>();
        CameraRigTr = CameraRig.GetComponent<Transform>();
        CameraTr = CameraCtrl.GetCameraTransform();

        thirdPersonViewWeapon = InitilizeHoldingWeapon(weaponHolderTr);

        if (UnityService == null)
        {
            UnityService = new UnityServiceManager();
        }
        weaponClass = fisrtPersonViewWeapon.GetComponent<WeaponAK74>();
        fisrtPersonViewWeapon.transform.parent = CameraTr;


        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;


        var components = fisrtPersonViewWeapon.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer component in components)
        {
            component.shadowCastingMode = ShadowCastingMode.Off;
        }

        this.UpdateTheTexturWhenCameraviewChange();

        weaponClass.OnShotFire += ShakeCameraWhenShoot;
        weaponClass.OnShotFire += StartReboundCoroutine;
        weaponClass.OnShotFire += SetWeaponFireAnimation;
        CameraCtrl.OnViewChange += UpdateTheTexturWhenCameraviewChange;

        IsHoldingRifle = true;

    }


    void Update()
    {
        //update scene

        UpdateUserInput();
        CurrentState.UpdateState(this);

        if (UnityService.GetMouseButtonUp(0))
        {

            this.Attack();
        }
    }



    void LateUpdate()
    {

        SetWeaponPosition();
        RotateNeck();
        RotateHeadAndAvatar(CameraCtrl.YRot);
    }
    #endregion

    #region animation method
    public void MoveAnimation(float xSpeed ,float zSpeed)
    {

        if(!isRunning && zSpeed > 0.5f)
        {
            zSpeed = 0.5f;
        }
        if(zSpeed <= 0f )
        {
            IsRunning = false;
        }

        Animator.SetFloat(hashXDirectionSpeed, xSpeed);
        Animator.SetFloat(hashZDirectionSpeed, zSpeed);

        if (((xSpeed > 0) || (xSpeed < 0) || (zSpeed > 0) || (zSpeed < 0)) || IsHoldingRifle)
            this.RotateAvatarTowardSight();// minus the amount of the value of angle to the head to lotate the body while moving the body .
    }

    public void SetWeaponFireAnimation()
    {
        Animator.SetTrigger(hashFire);
    }
    #endregion

    #region CharacterBodyMove

    private void RotateNeck()
    {
        //you might be wondering why did i put the x on z axis 
        //it is because the I found that local Transform of the Neck bone in model was reversed. 
        neckTr.localRotation = Quaternion.Euler(0f, 0f, CameraTr.localRotation.eulerAngles.x);

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

                PlayerTransform.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

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

                PlayerTransform.localRotation *= Quaternion.Euler(0, leftoverAngle, 0f);

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
        var bodyRot = PlayerTransform.localRotation;
        var lookRotation = Quaternion.LookRotation(CameraRigTr.forward);

        float angle = Quaternion.Angle(transform.rotation, lookRotation);
        float timeToComplete = angle / rotationSpeed;
        float donePercentage = Mathf.Min(1F, UnityService.DeltaTime / timeToComplete);

        PlayerTransform.localRotation = Quaternion.Slerp(PlayerTransform.localRotation, lookRotation, donePercentage);

        //take the amount of change in body rotation 
        var ChangeRotBody = PlayerTransform.localRotation.eulerAngles.y - bodyRot.eulerAngles.y;
        //add to Head Rotation
        headRot *= Quaternion.Euler(0f, -ChangeRotBody, 0f);
        actualHeadRot *= Quaternion.Euler(0f, -ChangeRotBody, 0f);
    }

    #endregion

    #region GunMoveMotion
    
    public void SetWeaponPosition()
    {
        Vector3 weaponPosition = new Vector3(weaponBob.x + weaponRebound.x + weaponOffX + weaponRebound.x,
            weaponBob.y + weaponRebound.y + weaponOffY , weaponBob.z + weaponRebound.z + weaponOffZ);
        fisrtPersonViewWeapon.transform.localPosition = weaponPosition;
    }

    public void UpdateWeaponBob(float xMovement,float YMovement)
    {
        if (UnityService.DeltaTime > 0f)
        {
            var bobFactor = (xMovement + YMovement * 0.5f) * 0.5f;

            float xBobValue = Mathf.Sin(UnityService.TimeAtFrame * BobFrequency) * BobAmount * bobFactor;
            float yBobValue = ((Mathf.Sin(UnityService.TimeAtFrame * BobFrequency * 2f) * 0.5f) + 0.5f) * BobAmount * bobFactor;

            weaponBob.x = xBobValue;
            weaponBob.y = Mathf.Abs(yBobValue);
        }
    }

    public void UpdateWeaponReboundWhenShoot()
    {
        if (UnityService.DeltaTime > 0f)
        {
            float xReboundValue = Mathf.Sin(UnityService.TimeAtFrame *90f) * weaponClass.ShakeMagnitudePos * 0.025f;

            float yReboundValue = Mathf.Sin(UnityService.TimeAtFrame * 70f) * weaponClass.ShakeMagnitudePos * 0.1f;

            float zReboundValue = Mathf.Sin(UnityService.TimeAtFrame * 50f) * weaponClass.ShakeMagnitudePos * 0.1f;

            weaponRebound.x = xReboundValue;
            weaponRebound.y = yReboundValue;
            weaponRebound.z = zReboundValue;
        }
    }

    public IEnumerator KeepUpdateWeaponRebound()
    {
        var timeAtShoot = UnityService.TimeAtFrame;
        //the UpdateRebound  will continue in corutine during delay
        while(timeAtShoot + weaponClass.Delay > UnityService.TimeAtFrame)
        {
            this.UpdateWeaponReboundWhenShoot();
            yield return null;
        }
    }

    public void StartReboundCoroutine()
    {
        StartCoroutine(KeepUpdateWeaponRebound());
    }

    public void ShakeCameraWhenShoot()
    {

        StartCoroutine(CameraCtrl.ShakeCamera(weaponClass.ShakeDuration, weaponClass.ShakeMagnitudePos, weaponClass.ShakeMagnitudeRot));
    }

    #endregion


    private GameObject InitilizeHoldingWeapon(Transform weaponHolderTr)
    {
        GameObject weaponTargetHolding = Instantiate(Resources.Load<GameObject>("GunPrefabs/AK74Weapon")) as GameObject;

        weaponTargetHolding.transform.parent = weaponHolderTr;
        weaponTargetHolding.transform.localScale = new Vector3(180f, 180f, 180f);
        weaponTargetHolding.transform.localPosition = new Vector3(26.7f, 7f, -3.4f);
        weaponTargetHolding.transform.localRotation = Quaternion.Euler(2.725f, -128.081f, 87.86401f);
        return weaponTargetHolding;
    }

    //check attack timer (so player do not "attack" every frames); 
    public bool CheckIsAttackReady(float duration)
    {
        StateTimeElapsed += UnityService.DeltaTime;
        return (StateTimeElapsed >= duration);

    }

    public void OnExitState()
    {
        StateTimeElapsed = 0;
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != RemainState)
        {
            CurrentState = nextState;
            this.OnExitState();
        }
    }

    public void Attack()
    {
        if(IsHoldingRifle)
        {
            weaponClass.Fire(CameraTr.transform.position, CameraTr.transform.forward);

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
            IsHoldingRifle = !IsHoldingRifle;
        }
    }

    //weapon
    public void UpdateTheTexturWhenCameraviewChange()
    {
        var currentViewMode = CameraCtrl.CurrentViewMode;
        var renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

        if (currentViewMode == CameraViewType.First)
        {
            renderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            this.SetFirstPersonWeaponVisible(true);
            this.SetThirdPersonWeaponVisible(false);
        }
        else if (currentViewMode == CameraViewType.Third)
        {
            renderer.shadowCastingMode = ShadowCastingMode.On;
            this.SetFirstPersonWeaponVisible(false);
            this.SetThirdPersonWeaponVisible(true);

        }
    }
    private void SetFirstPersonWeaponVisible(bool boolValue)
    {
        var weaponMeshs = fisrtPersonViewWeapon.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in weaponMeshs)
            mesh.enabled = boolValue;
    }
    private void SetThirdPersonWeaponVisible(bool boolValues)
    {
        var weaponMeshs = thirdPersonViewWeapon.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in weaponMeshs)
            mesh.enabled = boolValues;

    }
}





