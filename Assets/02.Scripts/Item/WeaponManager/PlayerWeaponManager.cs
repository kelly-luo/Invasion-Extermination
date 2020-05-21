using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerWeaponManager : MonoBehaviour, IWeaponManager
{

    public Vector3 WeaponOffSet { get; set; }

    private Vector3 weaponRebound;
    public Vector3 WeaponRebound
    {
        get { return weaponRebound; }
        set { weaponRebound = value; }
    }

    private Vector3 weaponBob;
    public Vector3 WeaponBob
    {
        get { return weaponBob; }
        set { weaponBob = value; }
    }

    public float BobFrequency { get; set; } = 10f;
    public float BobAmount { get; set; } = 0.05f;

    private GameObject currentWeaponObject;
    public GameObject CurrentWeaponObject
    {
        get
        {
            return currentWeaponObject;
        }
        set
        {
            // need to check type later 
            currentWeaponClass = value.GetComponent<ImWeapon>();
            currentWeaponObject = value;
        }
    }

    private ImWeapon currentWeaponClass;
    public ImWeapon FirstWeaponClass { get; set; }
    public ImWeapon ThirdWeaponClass { get; set; }

    public GameObject ThirdPersonViewWeapon { get; set; }
    public GameObject FirstPersonViewWeapon { get; set; }

    private IUnityServiceManager UnityService = UnityServiceManager.Instance;


    public void Attack(Vector3 playerPosition, Vector3 shootDirection)
    {
        FirstWeaponClass.Fire(playerPosition, shootDirection);
    }

    
    
    #region Initialize  

    public void EquipNewWeapon(GameObject FirstPeronViewWeapon, Transform fisrtPersonViewWeaponHolderTr, Transform thirdPersonViewWeaponHolderTr)
    {
        if (ThirdPersonViewWeapon != null)
        {
            GameObject.Destroy(ThirdPersonViewWeapon);
            FirstPersonViewWeapon.SetActive(false);
        }

        this.InitializeFirstPersonWeapon(FirstPeronViewWeapon, fisrtPersonViewWeaponHolderTr);
        this.InitializeThirdPersonWeapon(thirdPersonViewWeaponHolderTr);
    }

    private void InitializeThirdPersonWeapon(Transform thirdPersonViewWeaponHolderTr)
    {
        ThirdPersonViewWeapon = GameObject.Instantiate(FirstPersonViewWeapon, thirdPersonViewWeaponHolderTr);
        ThirdPersonViewWeapon.transform.localScale = new Vector3(180f, 180f, 180f);
        ThirdPersonViewWeapon.transform.localPosition = new Vector3(26.7f, 7f, -3.4f);
        ThirdPersonViewWeapon.transform.localRotation = Quaternion.Euler(2.725f, -128.081f, 87.86401f);
        ThirdWeaponClass = ThirdPersonViewWeapon.GetComponent<ImWeapon>();

    }

    private void InitializeFirstPersonWeapon(GameObject FirstPeronViewWeapon, Transform fisrtPersonViewWeaponHolderTr)
    {
        FirstPeronViewWeapon.SetActive(true);
        FirstPersonViewWeapon = FirstPeronViewWeapon;
        FirstPersonViewWeapon.transform.parent = fisrtPersonViewWeaponHolderTr;
        FirstPersonViewWeapon.transform.localScale = new Vector3(1f, 1f, 1f);
        WeaponOffSet = new Vector3(0.21f, -0.11f, 0.64f);
        FirstPersonViewWeapon.transform.localRotation = Quaternion.Euler(0f, 180f,0f);
        FirstWeaponClass = FirstPersonViewWeapon.GetComponent<ImWeapon>();
    }
    #endregion


    #region Update Weapon
    public void UpdateFirstPersonViewWeaponPosition()
    {
        Vector3 weaponPosition = new Vector3(weaponBob.x + WeaponRebound.x + WeaponOffSet.x + WeaponRebound.x,
            weaponBob.y + WeaponRebound.y + WeaponOffSet.y, weaponBob.z + WeaponRebound.z + WeaponOffSet.z);
        FirstPersonViewWeapon.transform.localPosition = weaponPosition;

    }

    public void UpdateWeaponReboundWhenShoot()
    {
        if (UnityService.DeltaTime > 0f)
        {
            float xReboundValue = Mathf.Sin(UnityService.TimeAtFrame * 90f) * FirstWeaponClass.ShakeMagnitudePos * 0.025f;

            float yReboundValue = Mathf.Sin(UnityService.TimeAtFrame * 70f) * FirstWeaponClass.ShakeMagnitudePos * 0.1f;

            float zReboundValue = Mathf.Sin(UnityService.TimeAtFrame * 50f) * FirstWeaponClass.ShakeMagnitudePos * 0.1f;

            weaponRebound.x = xReboundValue;
            weaponRebound.y = yReboundValue;
            weaponRebound.z = zReboundValue;
        }
    }

    public void UpdateWeaponBob(float XMovement, float YMovement)
    {
        if (UnityService.DeltaTime > 0f)
        {
            var bobFactor = (XMovement + YMovement * 0.5f) * 0.5f;

            float xBobValue = Mathf.Sin(UnityService.TimeAtFrame * BobFrequency) * BobAmount * bobFactor;
            float yBobValue = ((Mathf.Sin(UnityService.TimeAtFrame * BobFrequency * 2f) * 0.5f) + 0.5f) * BobAmount * bobFactor;

            weaponBob.x = xBobValue;
            weaponBob.y = Mathf.Abs(yBobValue);
        }
    }

    #endregion

    public void StartReboundCoroutine()
    {
        StartCoroutine(KeepUpdateWeaponRebound());
    }

    public IEnumerator KeepUpdateWeaponRebound()
    {
        var timeAtShoot = UnityService.TimeAtFrame;
        //the UpdateRebound  will continue in corutine during delay
        while (timeAtShoot + FirstWeaponClass.Delay > UnityService.TimeAtFrame)
        {
            this.UpdateWeaponReboundWhenShoot();
            yield return null;
        }
    }
    public void SetFirstPersonWeaponActive(bool boolValue)
    {
        FirstPersonViewWeapon.SetActive(boolValue);
    }

    public void SetThirdPersonWeaponActive(bool boolValue)
    {
        ThirdPersonViewWeapon.SetActive(boolValue);
    }

    public void AddOnShootFireEvent(Action eventMethod)
    {
        if (FirstWeaponClass != null)
        {
            FirstWeaponClass.OnShotFire += eventMethod;
            ThirdWeaponClass.OnShotFire += eventMethod;
        }
    }

    public void SetFirstPersonWeaponVisible(bool boolValue)
    {
        var weaponMeshs = FirstPersonViewWeapon.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in weaponMeshs)
            mesh.enabled = boolValue;
        if (boolValue) //Current Weapon can only visible 
            currentWeaponClass = FirstWeaponClass;
    }
    public void SetThirdPersonWeaponVisible(bool boolValue)
    {
        var weaponMeshs = ThirdPersonViewWeapon.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in weaponMeshs)
            mesh.enabled = boolValue;
        if (boolValue) //Current Weapon can only visible 
            currentWeaponClass = ThirdWeaponClass;
    }
}
