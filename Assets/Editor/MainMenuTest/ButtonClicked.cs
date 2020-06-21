using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClicked : MonoBehaviour
{
    [Test]
    public void frame_appears_Menu()
    {
        //ARRANGE
        MenuButton button = new MenuButton();
        bcMainMenu optionClick = new bcMainMenu();
        GameObject frame = new GameObject();
        frame.SetActive(false);
        optionClick.SetFrame(frame);
        button.SetButtonClicked(optionClick);
        //ACT
        button.OnPointerClick(null);
        //ASSERT
        bool actual = frame.activeSelf;
        Assert.AreEqual(true, actual);
    }


    [Test]
    public void frame_disappears_Menu()
    {
        //ARRANGE
        MenuButton button = new MenuButton();
        bcMainMenu optionClick = new bcMainMenu();
        GameObject frame = new GameObject();
        frame.SetActive(false);
        optionClick.SetFrame(frame);
        button.SetButtonClicked(optionClick);
        button.OnPointerClick(null);
        //ACT
        button.OnPointerClick(null);
        //ASSERT
        bool actual = frame.activeSelf;
        Assert.AreEqual(false, actual);
    }

    [Test]
    public void test_button_click_when_disable()
    {
        //ARRANGE
        MenuButton button = new MenuButton();
        bcMainMenu optionClick = new bcMainMenu();
        GameObject frame = new GameObject();
        frame.SetActive(false);
        optionClick.SetFrame(frame);
        button.SetButtonClicked(optionClick);

        //ACT
        button.DisableButton();
        button.OnPointerClick(null);
        //ASSERT
        bool actual = frame.activeSelf;
        Assert.AreEqual(false, actual);
    }


}
