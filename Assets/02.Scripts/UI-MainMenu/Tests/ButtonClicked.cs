using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    [Test]
    public void frame_appears_detects_click()
    {
        //ARRANGE
        IMyMenuButton button = Substitute.For<IMyMenuButton>();
        OptionClicked optionClick = new OptionClicked();
        GameObject frame = new GameObject();
        optionClick.setFrame(frame);
        button.setButtonClicked(optionClick);
        //ACT
        button.click();
        //ASSERT
        bool actual = frame.activeSelf;
        Assert.AreEqual(true, actual);
    }
}
