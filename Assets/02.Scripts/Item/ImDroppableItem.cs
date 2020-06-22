using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is loot Item interface all the loot should have this class.
public interface ImDropableItem
{
    Transform ObjectTransform { get; set; }

    Vector3 InitialScale { get; set; }

    void InitialSpawnLootPopUpEffect();

    void OnCollisionWithPlayer(GameObject Player);

}
