using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NSubstitute;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class UI_HUD_Test : MonoBehaviour
{
    public UIManager uiManager;

    [SetUp]
    public void SetUpTest()
    {
        uiManager = new UIManager();
        InventoryManager invManager = new InventoryManager();
        invManager.inventoryPanel = new GameObject();
        uiManager.invManager = invManager;
        uiManager.shopManager = new GameObject().AddComponent<ShopManager>();
        UIManager.MakeInvisble(uiManager.shopManager.gameObject);
        uiManager.healthObject = new GameObject();
        uiManager.GameMenuPanel = new GameObject();
        uiManager.Controls = new GameObject();

        UIManager.MakeInvisble(uiManager.GameMenuPanel);
    }

    [Test]
    public void Check_If_Health_does_not_go_above_100()
    {
        //ARRANGE
        uiManager.SetHealth(100);
        //ACT
        uiManager.SetHealth(999);
        //ASSERT
        int actual = uiManager.displayhealth;
        Assert.AreEqual(100,actual);
    }

    [Test]
    public void Check_If_Health_does_not_go_less_than_0()
    {
        //ARRANGE
        uiManager.SetHealth(100);
        //ACT
        uiManager.SetHealth(-999);
        //ASSERT
        int actual = uiManager.displayhealth;
        Assert.AreEqual(0, actual);
    }


    [Test]
    public void Check_If_Score_does_not_go_less_than_0()
    {
        //ARRANGE
        uiManager.SetScore(100);
        //ACT
        uiManager.SetScore(-999);
        //ASSERT
        int actual = uiManager.displayscore;
        Assert.AreEqual(0, actual);
    }

    [Test]
    public void Check_If_Money_does_not_go_less_than_0()
    {
        //ARRANGE
        uiManager.SetMoney(100);
        //ACT
        uiManager.SetMoney(-999);
        //ASSERT
        int actual = uiManager.displaymoney;
        Assert.AreEqual(0, actual);
    }

    [Test]
    public void Check_If_Ammo_does_not_go_less_than_0()
    {
        //ARRANGE
        uiManager.SetAmountAmmo(100);
        //ACT
        uiManager.SetAmountAmmo(-999);
        //ASSERT
        int actual = uiManager.displayamountammo;
        Assert.AreEqual(0, actual);
    }


    [Test]
    public void Format_has_0_in_front_when_less_than_double_digits()
    {
        //ARRANGE
   
        //ACT
        string actual = UIManager.FormatValue(3);
        //ASSERT
        Assert.AreEqual("03", actual);
    }

    [UnityTest]
    public IEnumerator Inventory_slot_Appears_when_press_key_i()
    {
        //Arrange
        uiManager.invManager.inventoryPanel.transform.localScale = Vector3.zero;

        //Act
        var unityService = Substitute.For<IUnityServiceManager>();
        unityService.GetKeyUp(KeyCode.I).Returns(true);
        uiManager.UnityService = unityService;

        uiManager.CheckInput();
        yield return null;

        //Assert
        float actual = uiManager.invManager.inventoryPanel.transform.localScale.x;
        Assert.AreEqual(1f, actual);
    }

    [UnityTest]
    public IEnumerator Inventory_slot_Disppears_when_press_key_i()
    {
        //Arrange
        uiManager.invManager.inventoryPanel.transform.localScale = Vector3.one;

        //Act
        var unityService = Substitute.For<IUnityServiceManager>();
        unityService.GetKeyUp(KeyCode.I).Returns(true);
        uiManager.UnityService = unityService;

        uiManager.CheckInput();
        yield return null;

        //Assert
        float actual = uiManager.invManager.inventoryPanel.transform.localScale.x;
        Assert.AreEqual(0f, actual);
    }


    [UnityTest]
    public IEnumerator Game_Menu_appears_when_press_key_Escape()
    {
        //Arrange
        uiManager.GameMenuPanel.transform.localScale = Vector3.zero;
        //Act
        var unityService = Substitute.For<IUnityServiceManager>();
        unityService.GetKeyUp(KeyCode.Escape).Returns(true);
        uiManager.UnityService = unityService;

        uiManager.CheckInput();
        yield return null;

        //Assert
        float actual = uiManager.GameMenuPanel.transform.localScale.x;
        Assert.AreEqual(1f, actual);
    }

    [UnityTest]
    public IEnumerator Game_Menu_disappears_when_press_key_Escape()
    {
        //Arrange
        //Arrange
        uiManager.GameMenuPanel.transform.localScale = Vector3.one;

        //Act
        var unityService = Substitute.For<IUnityServiceManager>();
        unityService.GetKeyUp(KeyCode.Escape).Returns(true);
        uiManager.UnityService = unityService;

        uiManager.CheckInput();
        yield return null;

        //Assert
        float actual = uiManager.GameMenuPanel.transform.localScale.x;
        Assert.AreEqual(0f, actual);
    }
}
