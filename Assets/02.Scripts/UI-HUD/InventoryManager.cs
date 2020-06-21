/*InventoryManager Class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles the GUI of the player's inventory
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  22.05.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  System support packages
 */
using System.Collections.Generic;
//UnityEngine support packages
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory PlayerInventory { get; set; }

    private int maxSlot;
    private int maxWeapons;
    private int currentSlotUsed;

    [SerializeField] public GameObject inventoryPanel;
    [SerializeField] private GameObject inventorySlotPanel;
    [SerializeField] private InventorySlots invslots;

    [SerializeField] public GameObject inventoryHoverPanel;

    [SerializeField] private GameObject weaponInstances;
    private GameObject[] weaponInstance;

    [SerializeField] private GameObject Primary;
    [SerializeField] private GameObject Secondary;

    public IUnityServiceManager UnityService { get; set; } = UnityServiceManager.Instance;

    /*
    * Intialize()
    *  ~~~~~~~~~~~~~~~~
    *  Intializes the class, and connects the player inventory to GUI inventory
    */
    public void Initialize(Inventory inventory)
    {
        PlayerInventory = inventory;
        inventoryHoverPanel.transform.localScale = Vector3.zero;

        currentSlotUsed = 0;
        maxSlot = inventorySlotPanel.transform.childCount;

        maxWeapons = weaponInstances.transform.childCount;
        weaponInstance = new GameObject[maxWeapons];
        for (int i = 0; i < maxWeapons; i++)
        {
            weaponInstance[i] = weaponInstances.transform.GetChild(i).gameObject;
        }
        UpdateWeaponSlots();

    }

    #region Inventory_Management
    /*
    * SetPrimary()
    *  ~~~~~~~~~~~~~~~~
    *  Select primary gun
    */
    public void SetPrimary(int key)
    {
        if (PlayerInventory.ContainsKey(key))
        {
            PlayerInventory.SetPrimary(key);
            UpdateWeaponSlots();
   
        }
    }
    /*
    * SetSecondary()
    *  ~~~~~~~~~~~~~~~~
    *  Select Secondary gun
    */
    public void SetSecondary(int key)
    {
        if (PlayerInventory.ContainsKey(key))
        {
            PlayerInventory.SetSecondary(key);
            UpdateWeaponSlots();
          
        }
    }
    /*
    * RemoveItem()
    *  ~~~~~~~~~~~~~~~~
    *  Removes gun from inventory
    */
    public void RemoveItem(int key)
    {

        if (PlayerInventory.ContainsKey(key))
        {
            ImItem RemovedItem = PlayerInventory.FindItem(key);
            PlayerInventory.Remove(RemovedItem);

            UpdateWeaponSlots();
        }
    }

    #endregion

    #region GUI_Management
    /*
    * UpdateWeaponSlots()
    *  ~~~~~~~~~~~~~~~~
    *  Updates the primary & secondary weapons slots
    */
    public void UpdateWeaponSlots()
    {
        if(PlayerInventory.Primary != null)
        {     
            Primary.GetComponent<Image>().sprite = GetImage(PlayerInventory.Primary.EntityID);
            if (!Primary.activeSelf) Primary.SetActive(true);
        }
        else
            Primary.SetActive(false);
        
        if (PlayerInventory.Secondary != null)
        {
            Secondary.GetComponent<Image>().sprite = GetImage(PlayerInventory.Secondary.EntityID);
            if (!Secondary.activeSelf) Secondary.SetActive(true);
        }else
            Secondary.SetActive(false);
    }
    /*
    * GetImage()
    *  ~~~~~~~~~~~~~~~~
    *  Gets the sprite of the gun
    */
    private Sprite GetImage(int id)
    {
        if (weaponInstance == null) return null;
        int EntityId;
        for (int i = 0; i < maxWeapons; i++)
        {
            EntityId = weaponInstance[i].GetComponent<Enity_ID>().EntityId;
            if(EntityId == id) return weaponInstance[i].GetComponent<SpriteRenderer>().sprite;
        }
        return null;
    }
    /*
    * UpdateInventoryGUI()
    *  ~~~~~~~~~~~~~~~~
    *  Updates the inventory slots, with the proper sprites and information
    */
    public void UpdateInventoryGUI()
    {
        UpdateWeaponSlots();
        currentSlotUsed = PlayerInventory.GetSize();
        int slotNo = -1;
        foreach (KeyValuePair<int, ImItem> Weapon in PlayerInventory.inventory)
        {
            slotNo++;
            invslots.slots[slotNo].SetActive(true);
            invslots.slots[slotNo].GetComponent<MenuButton>().EnableButton();

            bcSlotSelect slotInfo = invslots.slots[slotNo].GetComponent<bcSlotSelect>();

            slotInfo.InstanceId = Weapon.Key;
            slotInfo.SetSprite(GetImage(Weapon.Value.EntityID));
            slotInfo.stack_text.text = UIManager.FormatValue(Weapon.Value.StackAmount);
         
        }

        for (int i = PlayerInventory.GetSize(); i < invslots.slots.Length; i++)
        {
            invslots.slots[i].GetComponent<MenuButton>().DisableButton();
            invslots.slots[i].SetActive(false);
        }
    }
    /*
    * DisplayHoverPanel()
    *  ~~~~~~~~~~~~~~~~
    *  Display the gun information in the hover panel
    */
    public void DisplayHoverPanel(int key)
    {
        if (inventoryHoverPanel.transform.localScale.x == 0) inventoryHoverPanel.transform.localScale = Vector3.one;
        ImWeapon gunInfo = (ImWeapon)PlayerInventory.FindItem(key);

        HoverGunPanel displayvalues = inventoryHoverPanel.GetComponent<HoverGunPanel>();
        Sprite GunImage = GetImage(gunInfo.EntityID);

        if (GunImage != null)
        {
            displayvalues.setTitle(GunImage.name);
            displayvalues.setImage(GunImage);
        }

        if (PlayerInventory.Primary.InstanceID == key) displayvalues.setSelected("Primary");
        else if (PlayerInventory.Secondary.InstanceID == key) displayvalues.setSelected("Secondary");
        else displayvalues.setSelected("Not Selected");

        displayvalues.setText(0, gunInfo.Damage);//Display damage text
        displayvalues.setText(1, gunInfo.MaxBullet);//Display clip size

    }
    /*
    * HideHoverPanel()
    *  ~~~~~~~~~~~~~~~~
    *  Hides the hover panel
    */
    public void HideHoverPanel()
    {
        if (inventoryHoverPanel.transform.localScale.x == 1) inventoryHoverPanel.transform.localScale = Vector3.zero;
    }

    #endregion
    /*
    * Update()
    *  ~~~~~~~~~~~~~~~~
    *  Checks if the GUI inventory needs to updated to match the player's inventory
    */
    void Update()
    {

        if (PlayerInventory != null)
        {
            if (currentSlotUsed != PlayerInventory.GetSize()) UpdateInventoryGUI();
        }

    }
}
