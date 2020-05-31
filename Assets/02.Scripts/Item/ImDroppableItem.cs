using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImDropableItem
{
    GameObject ObjectPrefab { get; set; }

    Vector3 InitialScale { get; set; }

    void InitialSpawnImpulseEffect();

    void OnCollisionWithPlayer(GameObject Player);

}
