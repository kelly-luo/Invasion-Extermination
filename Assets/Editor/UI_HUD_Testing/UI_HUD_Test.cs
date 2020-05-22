using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NSubstitute;
using UnityEngine.UI;

public class UI_HUD_Test : MonoBehaviour
{
    public UIManager manager;

    [SetUp]
    public void Start()
    {
        manager = new UIManager();
    }

    [Test]
    public void Check_If_Health_does_not_go_above_100()
    {
        //ARRANGE
        manager.SetHealth(100);
        //ACT
        manager.SetHealth(999);
        //ASSERT
        int actual = manager.displayhealth;
        Assert.AreEqual(100,actual);
    }

    [Test]
    public void Check_If_Health_does_not_go_less_than_0()
    {
        //ARRANGE
        manager.SetHealth(100);
        //ACT
        manager.SetHealth(-999);
        //ASSERT
        int actual = manager.displayhealth;
        Assert.AreEqual(0, actual);
    }


    [Test]
    public void Check_If_Score_does_not_go_less_than_0()
    {
        //ARRANGE
        manager.SetScore(100);
        //ACT
        manager.SetScore(-999);
        //ASSERT
        int actual = manager.displayscore;
        Assert.AreEqual(0, actual);
    }

    [Test]
    public void Check_If_Money_does_not_go_less_than_0()
    {
        //ARRANGE
        manager.SetMoney(100);
        //ACT
        manager.SetMoney(-999);
        //ASSERT
        int actual = manager.displaymoney;
        Assert.AreEqual(0, actual);
    }

    [Test]
    public void Check_If_Ammo_does_not_go_less_than_0()
    {
        //ARRANGE
        manager.SetAmmo(100);
        //ACT
        manager.SetAmmo(-999);
        //ASSERT
        int actual = manager.displayammo;
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


    [Test]
    public void Inventory_slot_appears_when_press_key_i()
    {

    }
}
