using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMobFactory : MobFactory
{
    public GameObject pistol;
    public GameObject Alien;
    public GameObject Human;

    public override GameObject CreateMobWithWeapon(Vector3 SpawnLocation)
    {

        GameObject avatorThatReturn = Instantiate<GameObject>(Alien, SpawnLocation, Quaternion.identity);


        return avatorThatReturn;
    }
    public override GameObject CreateMob(Vector3 SpawnLocation)
    {

        GameObject avatorThatReturn = Instantiate(Human, SpawnLocation, Quaternion.identity) as GameObject;



        return avatorThatReturn;
    }
}
