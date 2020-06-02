using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    private IUnityServiceManager UnityServiceManager;
    public GameObject[] guns;
    public List<ShopItem> shopItems;

    public ShopItem CreateShopItem(int number)
    {
        ShopItem returnItem;
        var gun = guns[UnityServiceManager.UnityRandomRange(0, 4)].GetComponent<ImWeapon>();
        gun.Damage += UnityServiceManager.UnityRandomRange(0, (int)(gun.Damage * 0.1f));

        return returnItem = new ShopItem(gun, UnityServiceManager.UnityRandomRange());
    }


    public class ShopItem
    {
        ImItem item;
        int money = 1000;

        public ShopItem(ImItem item, int money)
        {
            this.item = item;
            this.money = money;
        }
    }
}
