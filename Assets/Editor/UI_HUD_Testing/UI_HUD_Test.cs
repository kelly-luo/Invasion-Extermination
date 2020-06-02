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
        uiManager.SetAmmo(100);
        //ACT
        uiManager.SetAmmo(-999);
        //ASSERT
        int actual = uiManager.displayammo;
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
        var uiManagertemp = new GameObject().AddComponent<UIManager>();
        uiManagertemp.invManager = new GameObject().AddComponent<InventoryManager>();
        uiManagertemp.invManager.inventoryPanel = new GameObject();
        uiManagertemp.invManager.inventoryPanel.transform.localScale = new Vector3(0, 0, 0);

        //Act
        var unityService = Substitute.For<IUnityServiceManager>();
        unityService.GetKeyUp(KeyCode.I).Returns(true);
        uiManagertemp.UnityService = unityService;

        uiManagertemp.CheckInput();
        yield return null;

        //Assert
        float actual = uiManagertemp.invManager.inventoryPanel.transform.localScale.x;
        Assert.AreEqual(1f, actual);
    }

    [UnityTest]
    public IEnumerator Inventory_slot_Disppears_when_press_key_i()
    {
        //Arrange
        var uiManagertemp = new GameObject().AddComponent<UIManager>();
        uiManagertemp.invManager = new GameObject().AddComponent<InventoryManager>();
        uiManagertemp.invManager.inventoryPanel = new GameObject();
        uiManagertemp.invManager.inventoryPanel.transform.localScale = new Vector3(1, 1, 1);

        //Act
        var unityService = Substitute.For<IUnityServiceManager>();
        unityService.GetKeyUp(KeyCode.I).Returns(true);
        uiManagertemp.UnityService = unityService;

        uiManagertemp.CheckInput();
        yield return null;

        //Assert
        float actual = uiManagertemp.invManager.inventoryPanel.transform.localScale.x;
        Assert.AreEqual(0f, actual);
    }


    [UnityTest]
    public IEnumerator Game_Menu_appears_when_press_key_Escape()
    {
        //Arrange
        var uiManagertemp = new GameObject().AddComponent<UIManager>();
        uiManagertemp.GameMenuPanel = new GameObject();
        uiManagertemp.GameMenuPanel.transform.localScale = new Vector3(0, 0, 0);

        //Act
        var unityService = Substitute.For<IUnityServiceManager>();
        unityService.GetKeyUp(KeyCode.Escape).Returns(true);
        uiManagertemp.UnityService = unityService;

        uiManagertemp.CheckInput();
        yield return null;

        //Assert
        float actual = uiManagertemp.GameMenuPanel.transform.localScale.x;
        Assert.AreEqual(1f, actual);
    }

    [UnityTest]
    public IEnumerator Game_Menu_disappears_when_press_key_Escape()
    {
        //Arrange
        var uiManagertemp = new GameObject().AddComponent<UIManager>();
        uiManagertemp.GameMenuPanel = new GameObject();
        uiManagertemp.GameMenuPanel.transform.localScale = new Vector3(1, 1, 1);

        //Act
        var unityService = Substitute.For<IUnityServiceManager>();
        unityService.GetKeyUp(KeyCode.Escape).Returns(true);
        uiManagertemp.UnityService = unityService;

        uiManagertemp.CheckInput();
        yield return null;

        //Assert
        float actual = uiManagertemp.GameMenuPanel.transform.localScale.x;
        Assert.AreEqual(0f, actual);
    }
}
