// This class is interface of projectile object 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImProjectile
{
    //Damage when collide with player
    float CollisionDamage { get; set; }

    float DestroyDelay { get; set; }
    //Is it cofirmed that this game object will destroyed?
    bool IsDisposing { get; set; }
    //Is it collide with any other object?
    bool IsCollideWithOther { get; set; } 

    //This method should called when player collide with this object
    void OnCollisionWithPlayer(GameObject Player);

    //This method should called when obstacle collide with this object
    void OnCollisionWithObstacle();

    void AfterThrow();
}