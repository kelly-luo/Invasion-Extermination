using NUnit.Framework;
using TMPro;
using UnityEngine;

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
        GameObject saveTextObject;
        GameObject loadTextObject;

        bcSave saveButton;
        bcLoad loadButton;

        [SetUp]
        public void SetUp()
        {
            saveTextObject = new GameObject();
            saveTextObject.AddComponent<TextMeshPro>();

            loadTextObject = new GameObject();
            loadTextObject.AddComponent<TextMeshPro>();

            saveButton = new bcSave();
            loadButton = new bcLoad();
        }

        // Below are tests for the SAVE text
        [Test]
        public void US49SaveLoadText_TestIfTextIsNotActiveBeforeSave()
        {
            Assert.IsFalse(saveTextObject.activeSelf);
        }

        [Test]
        public void US49SaveLoadText_TestIfTextIsActiveAfterSave()
        {
            saveButton.ButtonEvent(null);
            Assert.IsTrue(saveTextObject.activeSelf);
        }

        [Test]
        public void US49SaveLoadText_TestIfTextIsChangedAfterSave()
        {
            saveButton.ButtonEvent(null);

            string saveText = saveTextObject.GetComponent<TextMeshPro>().text;
            bool isSaveStringCorrect = saveText.Equals("Save complete");

            Assert.IsTrue(isSaveStringCorrect);
        }

        // Below are tests for the LOAD text
        [Test]
        public void US49SaveLoadText_TestIfTextIsNotActiveBeforeLoad()
        {
            Assert.IsFalse(loadTextObject.activeSelf);
        }

        [Test]
        public void US49SaveLoadText_TestIfTextIsActiveAfterLoad()
        {
            loadButton.ButtonEvent(null);
            Assert.IsTrue(loadTextObject.activeSelf);
        }

        [Test]
        public void US49SaveLoadText_TestIfTextIsChangedAfterLoad()
        {
            saveButton.ButtonEvent(null);

            string loadText = loadTextObject.GetComponent<TextMeshPro>().text;
            bool isLoadStringCorrect = loadText.Equals("Load complete");

            Assert.IsTrue(isLoadStringCorrect);
        }


    }
}
