//
// ShopItem
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// This class is a class that gives each item a cost to be sold in the Itemshop.
// When a ShopItem is instantiated, a random gun prefab is chosen, and using the gun
// prefab's base stats, 0 - 10% extra damage and max clip/magazine size is added.
// The gun's cost is also randomized from 900 - 1100.
//
// AUT university - 2020 - Howard Mao
//
using System;
using System.CodeDom;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ShopItem
{
    public IUnityServiceManager UnityService;
    public int costLowerRange = 900;
    public int costUpperRange = 1100;
    private const float PRECENTAGE = 0.1f;
    private const int ENTITYIDBUFFER = 1000;
    public int cost;
    public ImItem item;

    //
    // InstiateShopItem()
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // This method uses the CreateWeapon() method to first create a weapon, then randomize the cost.
    //
    // gunPrefabs       An array of GameObjects of gun prefabs
    //
    // returns          a ShopItem with the randomized gun and cost
    public ShopItem InstantiateShopItem(GameObject[] gunPrefabs)
    {
        UnityService = UnityServiceManager.Instance;
        this.item = CreateWeapon(gunPrefabs);
        this.cost = UnityService.UnityRandomRange(costLowerRange, costUpperRange);
        return this;
    }

    //
    // CreateWeapon()
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // This method randomly chooses a gun prefab from an array of gun prefabs,  then it
    // randomly gives the gun 0-10% extra damage and clip size.
    //
    // guns         An array of gun prefabs that the gun is chosen from
    // 
    // returns      An ImWeapon of the gun with the randomized values
    public ImWeapon CreateWeapon(GameObject[] guns)
    {
        var gun = GameObject.Instantiate(
            guns[UnityService.UnityRandomRange(0, guns.Length)], Vector3.zero, Quaternion.identity);
        var gunClass = gun.GetComponent<ImWeapon>();
        gun.transform.localScale = Vector3.zero;

        gunClass.Damage += UnityService.UnityRandomRange(0, (int)(gunClass.Damage * PRECENTAGE));
        gunClass.MaxBullet += UnityService.UnityRandomRange(0, (int)(gunClass.MaxBullet * PRECENTAGE));
        gunClass.InstanceID = gunClass.EntityID * ENTITYIDBUFFER + (int)gunClass.Damage;

        return gunClass;
    }
}
