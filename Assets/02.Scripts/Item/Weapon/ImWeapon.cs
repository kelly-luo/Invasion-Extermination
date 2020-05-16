using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImWeapon : ImItem
{

    GameObject WeaponObject { get;}

    float Damage { get; set; }

    int MaxBullet { get; }

    int NumOfBullet { get; set; }

    float Delay { get; }

    float ReloadTime { get; set; }

    float RequiredScore { get; }

    Action OnShotFire { get; set; }

    Action OnBulletRunOut { get; set; }
    
    Action OnReload { get; set; }

    GameObject Fire();

    void Reload(ref int numOfBulletLeft);
}
