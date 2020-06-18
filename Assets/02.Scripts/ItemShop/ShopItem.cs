using System;
using System.CodeDom;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ShopItem
{
    public IUnityServiceManager UnityService;
    public int costLowerRange = 900;
    public int costUpperRange = 1100;
    public int cost;
    public ImItem item;

    public ShopItem InstantiateShopItem(GameObject[] gunPrefabs)
    {
        UnityService = UnityServiceManager.Instance;
        this.item = CreateWeapon(gunPrefabs);
        this.cost = UnityService.UnityRandomRange(costLowerRange, costUpperRange);
        return this;
    }

    public ImWeapon CreateWeapon(GameObject[] guns)
    {
        var gun = GameObject.Instantiate(
            guns[UnityService.UnityRandomRange(0, guns.Length)], new Vector3(0f, 0f, 0f), Quaternion.identity);
        var gunClass = gun.GetComponent<ImWeapon>();
        gun.transform.localScale = new Vector3(0f, 0f, 0f);

        gunClass.Damage += UnityService.UnityRandomRange(0, (int)(gunClass.Damage * 0.1f));
        gunClass.MaxBullet += UnityService.UnityRandomRange(0, (int)(gunClass.MaxBullet * 0.1f));
        gunClass.InstanceID = gunClass.EntityID * 1000 + (int)gunClass.Damage;

        return gunClass;
    }
}
