/*ShopManager MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles the GUI for the itemshop
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  10.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  UnityEngine support packages
 */
using UnityEngine;
using UnityEngine.UI;
//Text Mesh Pro support packages
using TMPro;


public class ShopManager : MonoBehaviour
{
    public ItemShop itemShop;
    public ShopItem selecteditem;
    public int selectedindex;

    public Sprite[] sprites;
    public bcShopSlot[] slots;
    public GunPanel panel;

    private Sprite gunSprite;

    private int displayAmmo = 0;
    public TMP_Text ammo;

    private bool start = false;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        MakeGunPanel(0,true);
        UpdateInventory();
    }

    /*
    * Update()
    *  ~~~~~~~~~~~~~~~~
    *  Updates GUI for the shop, when itemshop changes
    */
    void Update()
    {
        if(itemShop.PlayerInfo.Ammo != displayAmmo)
        {
            displayAmmo = itemShop.PlayerInfo.Ammo;
            ammo.text = UIManager.FormatValue(displayAmmo);
        }

        if (itemShop.weaponsArray != null && !start)
        {
            start = true;
            MakeGunPanel(0,true);
            UpdateInventory();
        }
    }
    #region Shop_GUI
    /*
    * UpdateInventory()
    *  ~~~~~~~~~~~~~~~~
    *  Updates shop inventory GUI for the shop
    */
    public void UpdateInventory()
    {
        if(itemShop != null && itemShop.weaponsArray != null)
        {
            for (int i = 0; i < itemShop.weaponsArray.Length; i++)
            {
                ImWeapon weapon = (ImWeapon)itemShop.weaponsArray[i].item;
                slots[i].SetSlot(GetImage(weapon.EntityID), i);
            }
        }


    }
    /*
    * MakeGunPanel()
    *  ~~~~~~~~~~~~~~~~
    *  Updates shop gun GUI for the shop
    */
    public void MakeGunPanel(int index, bool select)
    {
        if (itemShop.weaponsArray != null)
        {
            ShopItem currentWeapon = itemShop.weaponsArray[index];
            if (select)
            {
                selectedindex = index;
                selecteditem = currentWeapon;
            }
            // ImWeapon weapon = (ImWeapon)selecteditem.item;
            if (currentWeapon.item is ImWeapon weapon)
            {
                gunSprite = GetImage(weapon.EntityID);
                panel.SetImage(gunSprite);
                panel.SetGunName(gunSprite.name);
                panel.SetGunDamage((int)weapon.Damage);
                panel.SetGunClipSize((int)weapon.MaxBullet);
                panel.SetGunPrice(selecteditem.cost);
            }

        }

    }
    #endregion

    #region Buying
    public void BuyGun()
    {
        itemShop.BuyItem(selectedindex);
        UpdateInventory();
        MakeGunPanel(0,true);
    }

    public void BuyAmmo()
    {
        itemShop.BuyAmmo();
    }
    #endregion

    /*
    * GetImage()
    *  ~~~~~~~~~~~~~~~~
    *  Get gun sprite
    */
    public Sprite GetImage(int id)
    {
        return sprites[id];
    }
}
