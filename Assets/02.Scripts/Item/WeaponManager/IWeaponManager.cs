using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponManager 
{

    Vector3 WeaponOffSet { get; set; }
    Vector3 WeaponRebound { get; set; }
    ImWeapon FirstWeaponClass { get; set; }
    GameObject CurrentWeaponObject { get; set; }

    void EquipNewWeapon(GameObject FirstPeronViewWeapon, Transform fisrtPersonViewWeaponHolderTr, Transform thirdPersonViewWeaponHolderTr);

    void UpdateWeaponBob(float XMovement, float YMovement);

    void UpdateFirstPersonViewWeaponPosition();

    void UpdateWeaponReboundWhenShoot();

    void SetFirstPersonWeaponActive(bool boolValue);

    void SetThirdPersonWeaponActive(bool boolValue);

    void SetFirstPersonWeaponVisible(bool boolValue);

    void SetThirdPersonWeaponVisible(bool boolValue);

    void Attack(Vector3 playerPosition, Vector3 shootDirection);

    void AddOnShootFireEvent(Action eventMethod);

}
