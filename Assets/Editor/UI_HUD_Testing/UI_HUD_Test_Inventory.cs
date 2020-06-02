using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NSubstitute;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class UI_HUD_Test_Inventory : MonoBehaviour
{
    public InventoryManager invManager;
    public bcSlotSelect menuButton;
    public HoverPanelValues hoverPanel;
    public Inventory inventory;

    [SetUp]
    public void SetUpTest()
    {
        invManager = new GameObject().AddComponent<InventoryManager>();
        menuButton = new GameObject().AddComponent<bcSlotSelect>();
        menuButton.inventoryManager = invManager;
        GameObject gbhoverpanel = new GameObject();
        gbhoverpanel.AddComponent<HoverPanelValues>();
        
        inventory = new Inventory();
        ImWeapon gun = new WeaponAK74();
        gun.InstanceID = 0;
        inventory.Add(gun);
        invManager.PlayerInventory = inventory;
        invManager.inventoryHoverPanel = gbhoverpanel;

        //Making makeshift display
        hoverPanel = invManager.inventoryHoverPanel.GetComponent<HoverPanelValues>();
        hoverPanel.textvalues = new TMP_Text[2];
        hoverPanel.textvalues[0] = Substitute.For<TMP_Text>();
        hoverPanel.textvalues[1] = Substitute.For<TMP_Text>();

        hoverPanel.gunName = Substitute.For<TMP_Text>();
        
    }
    [Test]
    public void Hover_Panel_Appears_When_User_Hover_Over_Item()
    {
        SetUpTest();
        menuButton.InstanceId = 0;
        
        menuButton.ButtonHover(null);
        hoverPanel.textvalues[0].text = "test";
        Debug.Log(hoverPanel.textvalues[0].text);
    }
    
}
