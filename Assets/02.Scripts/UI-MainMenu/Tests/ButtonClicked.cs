﻿using NUnit.Framework;
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
        MyMenuButton button = new MyMenuButton();
        OptionClicked optionClick = new OptionClicked();
        GameObject frame = new GameObject();
        frame.SetActive(false);
        optionClick.setFrame(frame);
        button.setButtonClicked(optionClick);
        //ACT
        button.click(null);
        //ASSERT
        bool actual = frame.activeSelf;
        Assert.AreEqual(true, actual);
    }


    [Test]
    public void frame_disappears_Menu()
    {
        //ARRANGE
        MyMenuButton button = new MyMenuButton();
        OptionClicked optionClick = new OptionClicked();
        GameObject frame = new GameObject();
        frame.SetActive(false);
        optionClick.setFrame(frame);
        button.setButtonClicked(optionClick);
        button.click(null);
        //ACT
        button.click(null);
        //ASSERT
        bool actual = frame.activeSelf;
        Assert.AreEqual(false, actual);
    }

}
