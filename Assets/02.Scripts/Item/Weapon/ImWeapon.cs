using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImWeapon : ImItem
{

    float Damage { get; set; }

    int MaxBullet { get; }

    int NumOfBullet { get; set; }

    float Delay { get; }

    bool IsShooting { get; }

    float ReloadTime { get; set; }

    float RequiredScore { get; }

    float ShakeDuration { get; }

    float ShakeMagnitudePos { get; }

    float ShakeMagnitudeRot { get; }

    Action OnShotFire { get; set; }

    Action OnBulletRunOut { get; set; }
    
    Action OnReload { get; set; }


    GameObject Fire(Vector3 playerPosition, Vector3 shootDirection);

    void SetLayer();

    void Reload(ref int numOfBulletLeft);
}
