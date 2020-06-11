using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopManager : MonoBehaviour
{
    public ItemShop itemShop;

    public Sprite[] sprites;
    public bcShopSlot[] slots;
    public GunPanel panel;

    private Sprite gunSprite;

    private int displayAmmo = 0;
    public TMP_Text ammo;
    void Start()
    {
        MakeGunPanel(itemShop.weaponsArray[0]);
        UpdateInventory();
    }

    
    void Update()
    {
        if(itemShop.PlayerInfo.Ammo != displayAmmo)
        {
            displayAmmo = itemShop.PlayerInfo.Ammo;
            ammo.text = UIManager.FormatValue(displayAmmo);
        }
    }

    public void UpdateInventory()
    {
       
        if(itemShop != null)
        {
            for (int i = 0; i < itemShop.weaponsArray.Length; i++)
            {
                ImWeapon weapon = (ImWeapon)itemShop.weaponsArray[i].item;
                slots[i].SetSlot(GetImage(weapon.EntityID), itemShop.weaponsArray[i]);
            }
        }
    }

    public void MakeGunPanel(ShopItem item)
    {
        ImWeapon weapon = (ImWeapon)item.item;
        gunSprite = GetImage(weapon.EntityID);
        panel.SetImage(gunSprite);
        panel.SetGunName(gunSprite.name);
        panel.SetGunDamage((int)weapon.Damage);
        panel.SetGunClipSize((int)weapon.MaxBullet);
        panel.SetGunPrice(item.cost);
    }

    public Sprite GetImage(int id)
    {
        return sprites[id];
    }
}
