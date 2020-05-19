using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using UnityEngine;
using UnityEngine.UI;

public class ControlsImageTest : MonoBehaviour
{
    [Test]
    public void controls_show_in_order_when_pressing_next()
    {
        //SETUP
        MyMenuButton button = new MyMenuButton();
        bcInstructions script = new bcInstructions();
        //ARRANGE
        script.InternalImageID = new int[3];
        for (int i = 0; i < 3; i++) script.InternalImageID[i] = i;
        button.setButtonClicked(script);
        //ACT
        button.click();
        //ASSERT
        Assert.AreEqual(1, script.CurrentIndex);
    }

    [Test]
    public void controls_go_back_to_first_insructions()
    {
        //SETUP
        MyMenuButton button = new MyMenuButton();
        bcInstructions script = new bcInstructions();
        //ARRANGE
        script.InternalImageID = new int[3];
        for (int i = 0; i < 3; i++) script.InternalImageID[i] = i;
        button.setButtonClicked(script);
        //ACT
        for(int i = 0; i < 3;i++) button.click();
        //ASSERT
        Assert.AreEqual(0, script.CurrentIndex);
    }
}
