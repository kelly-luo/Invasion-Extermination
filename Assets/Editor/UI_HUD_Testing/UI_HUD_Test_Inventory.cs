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
    public ImWeapon gun;

    [SetUp]
    public void SetUpTest()
    {
        invManager = new GameObject().AddComponent<InventoryManager>();
        menuButton = new GameObject().AddComponent<bcSlotSelect>();
        menuButton.inventoryManager = invManager;
        GameObject gbhoverpanel = new GameObject();
        gbhoverpanel.AddComponent<HoverPanelValues>();
        invManager.inventoryHoverPanel = gbhoverpanel;

        //Add temp guns into inventory for HUD to display
        inventory = new Inventory();
        for(int i = 0; i < 10; i++)
        {
            gun = new WeaponAK74();
            gun.InstanceID = i;
            inventory.Add(gun);
        }

        invManager.PlayerInventory = inventory;


        //Making makeshift display
        hoverPanel = invManager.inventoryHoverPanel.GetComponent<HoverPanelValues>();
        hoverPanel.textvalues = new TMP_Text[2];
        hoverPanel.textvalues[0] = Substitute.For<TMP_Text>();
        hoverPanel.textvalues[1] = Substitute.For<TMP_Text>();

        hoverPanel.gunName = Substitute.For<TMP_Text>();
        hoverPanel.selectedText = Substitute.For<TMP_Text>();
        hoverPanel.selectedImage = new GameObject().AddComponent<Image>();
    }
    [Test]
    public void Hover_Panel_Appears_When_User_Hover_Over_Item()
    {
        //Arrange
        menuButton.InstanceId = 0;
        invManager.inventoryHoverPanel.transform.localScale = new Vector3(0, 0, 0);
        //Act
        menuButton.ButtonHover(null);
        //Assert
        float actual = invManager.inventoryHoverPanel.transform.localScale.x;
        Assert.AreEqual(1f, actual);
    }

    [Test]
    public void Hover_Panel_Disappears_When_User_Exits_Hover_Over_Item()
    {
        //Arrange
        menuButton.InstanceId = 0;
        invManager.inventoryHoverPanel.transform.localScale = new Vector3(1, 1, 1);
        //Act
        menuButton.ButtonHoverExit(null);
        //Assert
        float actual = invManager.inventoryHoverPanel.transform.localScale.x;
        Assert.AreEqual(0f, actual);
    }

    [Test]
    public void Hover_Panel_Displays_Gun_Damage_Stat()
    {
        //Arrange
        menuButton.InstanceId = 0;
        //Act
        menuButton.ButtonHover(null);
        //Assert
        HoverPanelValues temp = invManager.inventoryHoverPanel.GetComponent<HoverPanelValues>();
        string actual = temp.textvalues[0].text;
        Assert.AreEqual("" + gun.Damage, actual);
    }

    [Test]
    public void Hover_Panel_Displays_Gun_ClipSize_Stat()
    {
        //Arrange
        menuButton.InstanceId = 0;
        //Act
        menuButton.ButtonHover(null);
        //Assert
        HoverPanelValues temp = invManager.inventoryHoverPanel.GetComponent<HoverPanelValues>();
        string actual = temp.textvalues[1].text;
        Assert.AreEqual("" + gun.MaxBullet, actual);
    }

    [Test]
    public void Hover_Panel_Displays_Gun_Is_Currently_Primary()
    {
        //Arrange
        menuButton.InstanceId = 0;
        //Act
        menuButton.ButtonHover(null);
        //Assert
        HoverPanelValues temp = invManager.inventoryHoverPanel.GetComponent<HoverPanelValues>();
        string actual = temp.selectedText.text;
        Assert.AreEqual("Primary", actual);
    }

    [Test]
    public void Hover_Panel_Displays_Gun_Is_Currently_Secondary()
    {
        //Arrange
        menuButton.InstanceId = 1;
        //Act
        menuButton.ButtonHover(null);
        //Assert
        HoverPanelValues temp = invManager.inventoryHoverPanel.GetComponent<HoverPanelValues>();
        string actual = temp.selectedText.text;
        Assert.AreEqual("Secondary", actual);
    }

    [Test]
    public void Hover_Panel_Displays_Gun_Is_Currently_Not_Selected()
    {
        //Arrange
        menuButton.InstanceId = 2;
        //Act
        menuButton.ButtonHover(null);
        //Assert
        HoverPanelValues temp = invManager.inventoryHoverPanel.GetComponent<HoverPanelValues>();
        string actual = temp.selectedText.text;
        Assert.AreEqual("Not Selected", actual);
    }

}
