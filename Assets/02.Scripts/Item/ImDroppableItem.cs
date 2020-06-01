using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImDropableItem
{
    Transform ObjectTransform { get; set; }

    Vector3 InitialScale { get; set; }

    void InitialSpawnLootPopUpEffect();

    void OnCollisionWithPlayer(GameObject Player);

}
