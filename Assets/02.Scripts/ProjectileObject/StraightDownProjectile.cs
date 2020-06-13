// This class is just for object that will used as falling projectile which go pierce all the obstacle 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightDownProjectile : MonoBehaviour,ImProjectile
{
    public float CollisionDamage { get; set; } = 100f;
    public float DestroyDelay { get; set; } = 0.1f;

    public bool IsDisposing { get; set; } = false;
    public bool IsCollideWithOther { get; set; } = false;

    void OnCollisionEnter(Collision coll)
    {

        if (coll.collider.CompareTag("Player"))
        {
            OnCollisionWithHuman(coll.gameObject);
        }
        if (coll.collider.CompareTag("Player"))
        {
            OnCollisionWithPlayer(coll.gameObject);
        }
    }

    //This method is usless for this projectile as this projectile will pierce through them
    public void OnCollisionWithObstacle()
    {

        return;
    }
    //When car collide with Human unit they also get pushed 
    public void OnCollisionWithHuman(GameObject human)
    {
        MonsterController stateController = human.GetComponent<MonsterController>();
        if (stateController != null)
        {
            stateController.TakeDamage(CollisionDamage);
        }
        return;
    }

    private IEnumerator SelfDestroy(float delay)
    {
        yield return null;
        Destroy(this.gameObject, delay);
    }

    //This method allow player take damage when they collide with this gameobject
    public void OnCollisionWithPlayer(GameObject player)
    {
        PlayerStateController stateController = player.GetComponent<PlayerStateController>();
        if (stateController != null)
        {
            stateController.TakeDamage(CollisionDamage);
        }
    }

    //self disposing after jobs done
    public void AfterThrow()
    {
        this.IsDisposing = true;
        StartCoroutine(SelfDestroy(DestroyDelay));
    }
}
