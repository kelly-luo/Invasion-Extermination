// This class is just for object that will used as Direct projectile which go pierce all the obstacle 
// main function is when they collide with player or obstacle they do different thing
// e.g. take damage
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectProjectile : MonoBehaviour, ImProjectile
{
    public float CollisionDamage { get; set; } = 25f;
    public float DestroyDelay { get; set; } = 0.1f;

    public bool IsDisposing { get; set; } = false;
    public bool IsCollideWithOther { get; set; } = false;

    private float explosionPower= 30f;
    private float explosionRadius = 3.2f;
    private float upForce = 5f;
    void OnCollisionEnter(Collision coll)
    {

        if (coll.collider.CompareTag("Player"))
        {
            OnCollisionWithPlayer(coll.gameObject);
        }
        if (coll.collider.CompareTag("Human"))
        {
            OnCollisionWithHuman(coll.gameObject);
        }
        if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            OnCollisionWithObstacle();
        }
    }

    //This method call destory method when they collide with Obstacle
    public void OnCollisionWithObstacle()
    {
        return;
    }
    //When car collide with Human unit they also get pushed 
    public void OnCollisionWithHuman(GameObject human)
    {
        ExplosiveForce(human);
        return;
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
        if (stateController != null)
        {
            ExplosiveForce(Player);
            stateController.TakeDamage(CollisionDamage);
        }
    }
    //using explosionForce to implement Car hit Kb
    private void ExplosiveForce(GameObject targetGameObject)
    {
        var rb = targetGameObject.GetComponent<Rigidbody>();
        if(rb != null)
            rb.AddExplosionForce(30f, gameObject.transform.position, explosionRadius
                , upForce, ForceMode.Impulse);
    }


    public void AfterThrow()
    {
        this.IsDisposing = true;
        StartCoroutine(SelfDestroy(DestroyDelay));
    }
}
