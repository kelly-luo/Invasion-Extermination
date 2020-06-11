// This class is just for object that will used as projectile
// main function is when they collide with player or obstacle they do different thing
// e.g. take damage
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : MonoBehaviour, ImProjectile
{
    public float CollisionDamage { get; set; } = 50f;
    public float DestroyDelay { get; set; } = 1f;

    public bool IsDisposing { get; set; } = false;
    public bool IsCollideWithOther { get; set; } = false;

    private Rigidbody rigidbody { get; set; }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision coll)
    {
        IsCollideWithOther = true;
        SetGravity(true);
        if (coll.collider.CompareTag("Player"))
        {
            OnCollisionWithPlayer(coll.gameObject);  
        }
        if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            OnCollisionWithObstacle();
        }
    }

    //This method call destory method when they collide with Obstacle
    public void OnCollisionWithObstacle()
    {
        IsDisposing = true;
        StartCoroutine(SelfDestroy(DestroyDelay));
    }

    private IEnumerator SelfDestroy(float delay)
    {
        yield return null;
        Destroy(this.gameObject, delay);
    }

    //This method allow player take damage when they collide with this gameobject
    public void OnCollisionWithPlayer(GameObject Player)
    {
        PlayerStateController stateController = Player.GetComponent<PlayerStateController>();
        if(stateController != null)
        {
            stateController.TakeDamage(CollisionDamage);
        }
    }

    public void SetGravity(bool SetGravityOn)
    {
        rigidbody.useGravity = SetGravityOn;
    }
}
