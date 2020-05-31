using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour, ImDropableItem
{

    public GameObject ObjectPrefab { get; set; }

    public Vector3 InitialScale { get; set; }

    public double MoneyAmonut { get; set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        throw new System.NotImplementedException();
    }

    void OnDisable()
    {
        throw new System.NotImplementedException();
    }

    void OnCollisionEnter(Collision coll)
    {
        throw new System.NotImplementedException();
    }

    public void InitialSpawnImpulseEffect()
    {
        throw new System.NotImplementedException();
    }

    public void OnCollisionWithPlayer(GameObject Player)
    {
        throw new System.NotImplementedException();
    }

}
