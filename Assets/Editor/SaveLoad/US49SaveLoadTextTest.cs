using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    //
    // US49SaveLoadTextTest
    // ~~~~~~~~~~~~~~~~~~~~
    // This test class is used to test user story 49: As a player I want to have a UI pop up so that I know I have completed my save or load successfully
    // 
    // AUT University - 2020 - Kelly Luo
    // 
    // Revision History
    // ~~~~~~~~~~~~~~~~
    // 5.06.2020 Creation date
    //
    public class US49SaveLoadTextTest
    {
        GameObject saveText;
        GameObject loadText;

        bcSave saveButton;
        bcLoad loadButton;

        [SetUp]
        public void SetUp()
        {
            saveText = new GameObject();
            saveText.AddComponent<TextMeshPro>();

            loadText = new GameObject();
            loadText.AddComponent<TextMeshPro>();

            saveButton = new bcSave();
            loadButton = new bcLoad();
        }

        // This test will check if the save completed text is not visible before saving button is pressed
        [Test]
        public void US49SaveLoadText_TestIfTextIsNotActiveBeforeSave()
        {
            Assert.IsFalse(saveText.activeSelf);
        }

        // This test will check if the save completed text is active (has appeared) after saving button is pressed
        [Test]
        public void US49SaveLoadText_TestIfTextIsActiveAfterSave()
        {
            saveButton.ButtonEvent(null);
            Assert.IsTrue(saveText.activeSelf);
        }

        // This test will check if the load completed text is not visible before load button is pressed
        [Test]
        public void US49SaveLoadText_TestIfTextIsNotActiveBeforeLoad()
        {
            Assert.IsFalse(loadText.activeSelf);
        }

        // This test will check if the load completed text is active (has appeared) after load button is pressed
        [Test]
        public void US49SaveLoadText_TestIfTextIsActiveAfterLoad()
        {
            loadButton.ButtonEvent(null);
            Assert.IsTrue(loadText.activeSelf);
        }


    }
}
