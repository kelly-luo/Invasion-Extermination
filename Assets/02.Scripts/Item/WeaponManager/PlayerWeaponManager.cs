using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : IWeaponManager
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
    private ImWeapon firstWeaponClass;
    private GameObject thirdPersonViewWeapon;
    private GameObject firstPersonViewWeapon;

   
    private IUnityServiceManager UnityService;

    public PlayerWeaponManager(IUnityServiceManager unityService)
    {
        this.UnityService = unityService;
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

    public void Attack(Vector3 playerPosition, Vector3 shootDirection)
    {
        currentWeaponClass.Fire(playerPosition, shootDirection);
    }

    private void InitializeThirdPersonWeapon(Transform thirdPersonViewWeaponHolderTr)
    {
        thirdPersonViewWeapon = GameObject.Instantiate(firstPersonViewWeapon, thirdPersonViewWeaponHolderTr);
        thirdPersonViewWeapon.transform.localScale = new Vector3(180f, 180f, 180f);
        thirdPersonViewWeapon.transform.localPosition = new Vector3(26.7f, 7f, -3.4f);
        thirdPersonViewWeapon.transform.localRotation = Quaternion.Euler(2.725f, -128.081f, 87.86401f);
    }
    private void InitializeFirstPersonWeapon(GameObject FirstPeronViewWeapon, Transform fisrtPersonViewWeaponHolderTr)
    {
        firstPersonViewWeapon = FirstPeronViewWeapon;
        firstPersonViewWeapon.transform.parent = fisrtPersonViewWeaponHolderTr;
        firstPersonViewWeapon.transform.localScale = new Vector3(1f, 1f, 1f);
        firstPersonViewWeapon.transform.localPosition = new Vector3(0.21f, -0.11f, 0.64f);
        firstPersonViewWeapon.transform.localRotation = Quaternion.Euler(2.725f, -128.081f, 87.86401f);
        firstWeaponClass = firstPersonViewWeapon.GetComponent<ImWeapon>();
    }

    public void EquipNewWeapon(GameObject FirstPeronViewWeapon, Transform fisrtPersonViewWeaponHolderTr, Transform thirdPersonViewWeaponHolderTr)
    {
        this.InitializeFirstPersonWeapon(FirstPeronViewWeapon, fisrtPersonViewWeaponHolderTr);
        this.InitializeThirdPersonWeapon(thirdPersonViewWeaponHolderTr);
    }

    public void UpdateFirstPersonViewWeaponPosition()
    {
        Vector3 weaponPosition = new Vector3(weaponBob.x + WeaponRebound.x + WeaponOffSet.x + WeaponRebound.x,
            weaponBob.y + WeaponRebound.y + WeaponOffSet.y, weaponBob.z + WeaponRebound.z + WeaponOffSet.z);
        firstPersonViewWeapon.transform.localPosition = weaponPosition;
    }

    public void UpdateWeaponReboundWhenShoot()
    {
        if (UnityService.DeltaTime > 0f)
        {
            float xReboundValue = Mathf.Sin(UnityService.TimeAtFrame * 90f) * firstWeaponClass.ShakeMagnitudePos * 0.025f;

            float yReboundValue = Mathf.Sin(UnityService.TimeAtFrame * 70f) * firstWeaponClass.ShakeMagnitudePos * 0.1f;

            float zReboundValue = Mathf.Sin(UnityService.TimeAtFrame * 50f) * firstWeaponClass.ShakeMagnitudePos * 0.1f;

            weaponRebound.x = xReboundValue;
            weaponRebound.y = yReboundValue;
            weaponRebound.z = zReboundValue;
        }
    }


    public void SetFirstPersonWeaponActive(bool boolValue)
    {
        firstPersonViewWeapon.SetActive(boolValue);
    }

    public void SetThirdPersonWeaponActive(bool boolValue)
    {
        thirdPersonViewWeapon.SetActive(boolValue);
    }
}
