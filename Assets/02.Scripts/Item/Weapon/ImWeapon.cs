using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImWeapon : ImItem
{
    float Damage { get; set; }

    float MaxBullet { get; }

    float Delay { get; }

    float RequiredScore { get; set; }

    
}
