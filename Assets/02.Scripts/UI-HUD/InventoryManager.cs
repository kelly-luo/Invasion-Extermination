using System.Collections;
using System.Collections.Generic;
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
    private GameObject[] slots;

    [SerializeField] public GameObject inventoryHoverPanel;

    [SerializeField] private GameObject weaponInstances;
    private GameObject[] weaponInstance;

    [SerializeField] private GameObject Primary;
    [SerializeField] private GameObject Secondary;

    public IUnityServiceManager UnityService { get; set; } = UnityServiceManager.Instance;

    public void Intialize(Inventory inventory)
    {
        PlayerInventory = inventory;
        inventoryHoverPanel.transform.localScale = new Vector3(0, 0, 0);

        currentSlotUsed = 0;
        maxSlot = inventorySlotPanel.transform.childCount;
        slots = new GameObject[maxSlot];

        for (int i = 0; i < maxSlot; i++)
        {
            slots[i] = inventorySlotPanel.transform.GetChild(i).gameObject;
        }

        maxWeapons = weaponInstances.transform.childCount;
        weaponInstance = new GameObject[maxWeapons];
        for (int i = 0; i < maxWeapons; i++)
        {
            weaponInstance[i] = weaponInstances.transform.GetChild(i).gameObject;
        }
        UpdateWeaponSlots();

    }




    #region Inventory_Management
    public void SetPrimary(int key)
    {
        if (PlayerInventory.ContainsKey(key))
        {
            PlayerInventory.SetPrimary(key);
            UpdateWeaponSlots();
   
        }
    }

    public void SetSecondary(int key)
    {
        if (PlayerInventory.ContainsKey(key))
        {
            PlayerInventory.SetSecondary(key);
            UpdateWeaponSlots();
          
        }
    }

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

    public void UpdateInventoryGUI()
    {
        UpdateWeaponSlots();
        currentSlotUsed = PlayerInventory.GetSize();
        int slotNo = -1;
        foreach (KeyValuePair<int, ImItem> Weapon in PlayerInventory.inventory)
        {
            slotNo++;
            slots[slotNo].SetActive(true);
            slots[slotNo].GetComponent<MenuButton>().EnableButton();
            // Debug.Log(slotNo + " : " + Weapon.Key + " , " + Weapon.Value.Id);

            bcSlotSelect slotInfo = slots[slotNo].GetComponent<bcSlotSelect>();

            slotInfo.InstanceId = Weapon.Key;
            slotInfo.setSprite(GetImage(Weapon.Value.EntityID));
            slotInfo.stack_text.text = UIManager.FormatValue(Weapon.Value.StackAmount);
         
        }

        for (int i = PlayerInventory.GetSize(); i < slots.Length; i++)
        {
            slots[i].GetComponent<MenuButton>().DisableButton();
            slots[i].SetActive(false);
        }
    }

    public void DisplayHoverPanel(int key)
    {
        if (inventoryHoverPanel.transform.localScale.x == 0) inventoryHoverPanel.transform.localScale = new Vector3(1, 1, 1);
        ImWeapon gunInfo = (ImWeapon)PlayerInventory.FindItem(key);

        HoverPanelValues displayvalues = inventoryHoverPanel.GetComponent<HoverPanelValues>();
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

    public void HideHoverPanel()
    {
        if (inventoryHoverPanel.transform.localScale.x == 1) inventoryHoverPanel.transform.localScale = new Vector3(0, 0, 0);
    }

    #endregion
    void Update()
    {

        if (PlayerInventory != null)
        {
            if (currentSlotUsed != PlayerInventory.GetSize()) UpdateInventoryGUI();
        }

    }
}
