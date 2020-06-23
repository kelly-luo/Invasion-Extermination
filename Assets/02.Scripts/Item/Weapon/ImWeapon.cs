using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is interface for Weapon Item it this object have amount of magnitude for rebound of gun
/// and others for the Object 
/// and SetLayers for Set the Layer where the Bullet can hit.
/// </summary>
public interface ImWeapon : ImItem
{

    float Damage { get; set; }

    float FiringRange { get; set; }

    int MaxBullet { get; set; }

    int NumOfBullet { get; set; }

    float Delay { get; }

    float ShootKnockbackVector { get; set; }

    bool IsShooting { get; }

    float ReloadDuration { get; set; }

    float RequiredScore { get; }

    float ShakeDuration { get; }

    float ShakeMagnitudePos { get; }

    float ShakeMagnitudeRot { get; }

    Action OnShotFire { get; set; }

    Action OnBulletRunOut { get; set; }
    
    Action OnReload { get; set; }

    float SoundVolume { get; set; }

    GameObject Fire(Vector3 playerPosition, Vector3 shootDirection);

    void SetLayer();

    void Reload(ref int ammoLeft);
}
