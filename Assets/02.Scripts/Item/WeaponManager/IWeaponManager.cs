using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This interface of weapon manager which allows gameobject to use gun or weapon with current camera View.
public interface IWeaponManager 
{
    bool IsReloading { get; set; }
    Vector3 WeaponOffSet { get; set; }
    Vector3 WeaponRebound { get; set; }
    ImWeapon FirstWeaponClass { get; set; }
    GameObject CurrentWeaponObject { get; set; }

    void StartReload(ref int ammoLeft);

    void EquipNewWeapon(GameObject FirstPeronViewWeapon, Transform fisrtPersonViewWeaponHolderTr, Transform thirdPersonViewWeaponHolderTr);

    void UnEquipCurrentWeapon();

    void UpdateWeaponBob(float XMovement, float YMovement);

    void UpdateFirstPersonViewWeaponPosition();

    void UpdateWeaponReboundWhenShoot();
    
    void SetFirstPersonWeaponActive(bool boolValue);

    void SetThirdPersonWeaponActive(bool boolValue);

    void SetFirstPersonWeaponVisible(bool boolValue);

    void SetThirdPersonWeaponVisible(bool boolValue);

    IEnumerator Attack(Vector3 playerPosition, Vector3 shootDirection);

    void AddOnShootFireEvent(Action eventMethod);


}
