using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSubstitute;
using UnityEngine.UI;
using UnityEngine.TestTools;
using NUnit.Framework;

public class UI_HealthBar_Test : MonoBehaviour
{
    public EnemyHealthBarController hcontrol;
    public EnemyHealthBar healthBar;
    [SetUp]
    public void SetUpTest()
    {
        hcontrol = new GameObject().AddComponent<EnemyHealthBarController>();
        GameObject tempTarget = new GameObject();
        hcontrol.targetTr = tempTarget.transform;
        hcontrol.rectParent = new GameObject().AddComponent<RectTransform>();
        hcontrol.rectHp = new GameObject().AddComponent<RectTransform>();
   
        hcontrol.screenpos = Vector3.one;

        healthBar = new GameObject().AddComponent<EnemyHealthBar>();
        healthBar.hpBarPrefab = new GameObject();
        healthBar.UiCanvas = new Canvas();
        healthBar.HpSlider = new GameObject().AddComponent<Slider>();
    }

    [Test]
    public void Health_Bar_Is_Visible()
    {
        //Arrange
        IUnityServiceManager service = Substitute.For<IUnityServiceManager>();
        hcontrol.targetTr.position = new Vector3(10, 10, 10);
        hcontrol.rectHp.transform.localScale = Vector3.zero;
        service.GetMainCameraPosition.Returns(Vector3.one);
        hcontrol.UnityServiceManager = service;
        //Act
        hcontrol.healthVisible();
        //Assert
        Vector3 actual = hcontrol.rectHp.transform.localScale;
        Assert.AreEqual(Vector3.one, actual);
    }

    [Test]
    public void Health_Bar_Is_Not_Visible()
    {
        //Arrange
        IUnityServiceManager service = Substitute.For<IUnityServiceManager>();
        hcontrol.targetTr.position = new Vector3(100, 100, 100);
        hcontrol.rectHp.transform.localScale = Vector3.one;
        service.GetMainCameraPosition.Returns(Vector3.one);
        hcontrol.UnityServiceManager = service;
        //Act
        hcontrol.healthVisible();
        //Assert
        Vector3 actual = hcontrol.rectHp.transform.localScale;
        Assert.AreEqual(Vector3.zero, actual);
    }

    [Test]
    public void Convert_Damage_To_Percentage()
    {
        //Arrange
        float tempHealh = 56.0f;
        //Act
        healthBar.onDamage(tempHealh);
        //Assert
        float actual = healthBar.DisplayHp;
        Assert.AreEqual(0.56f, actual);
    }

    [Test]
    public void Make_Slider_NULL_If_Health_Is_Zero()
    {
        //Arrange
        float tempHealh = 0f;
        //Act
        healthBar.onDamage(tempHealh);
        //Assert
        Slider actual = healthBar.HpSlider;
        Assert.AreEqual(null, actual);
    }

    [Test]
    public void Make_Slider_NULL_If_Health_Is_Less_Than_Zero()
    {
        //Arrange
        float tempHealh = -100.0f;
        //Act
        healthBar.onDamage(tempHealh);
        //Assert
        Slider actual = healthBar.HpSlider;
        Assert.AreEqual(null, actual);
    }



}
