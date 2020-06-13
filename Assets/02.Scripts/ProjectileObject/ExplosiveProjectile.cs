// This class is just for object that will used as Explosive projectile which explode when they hit something
// main function is when they explode they will give a push force and damage to surround player and Human 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

public class ExplosiveProjectile : MonoBehaviour, ImProjectile
{
    public float CollisionDamage { get; set; } = 5f;
    public float ExplosionDamage { get; set; } = 30f;
    public float DestroyDelay { get; set; } = 0.01f;

    public bool IsDisposing { get; set; } = false;
    public bool IsCollideWithOther { get; set; } = false;

    private float explosionPower = 20f;
    private float explosionRadius = 3.7f;
    private float upForce = 2.5f;
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
        this.ProjectileExplosion();
        this.SelfDestroy(DestroyDelay);
        return;
    }
    //When car collide with Human unit they also get pushed 
    public void OnCollisionWithHuman(GameObject human)
    {
        this.ProjectileExplosion();
        this.SelfDestroy(DestroyDelay);
        return;
    }
    //Self Destory with delay
    private IEnumerator SelfDestroy(float delay)
    {
        this.IsDisposing = true;
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    //This method allow player take damage when they collide with this gameobject
    public void OnCollisionWithPlayer(GameObject Player)
    {
        PlayerStateController stateController = Player.GetComponent<PlayerStateController>();
        if (stateController != null)
        {
            this.ProjectileExplosion();
            stateController.TakeDamage(CollisionDamage);

        }
    }
    //explosion will give force and damge to Player and Human type
    public void ProjectileExplosion()
    {
        Vector3 explosionPosition = gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider hit in colliders)
        {
            if(hit.CompareTag("Player")|| hit.CompareTag("Human"))
            {
                var stateClass = hit.GetComponent<IStateController>();
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (stateClass != null && rb != null)
                {
                    rb.AddExplosionForce(explosionPower, explosionPosition, explosionRadius, upForce, ForceMode.Impulse);
                    stateClass.TakeDamage(ExplosionDamage);
                }
            }
        }
    }

    public void AfterThrow()
    {
        this.IsDisposing = true;
        this.ProjectileExplosion();
        StartCoroutine(SelfDestroy(DestroyDelay));
    }
}
