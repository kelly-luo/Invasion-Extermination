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

//
// NUnit support packages
// ~~~~~~~~~~~~~~~~~~~~~
using NUnit.Framework;
//
// Unity support packages
// ~~~~~~~~~~~~~~~~~~~~~
using TMPro;
using UnityEngine;

namespace Tests
{
    public class US49SaveLoadTextTest
    {
        GameObject player;
        PlayerInformation stats;

        GameObject saveTextObject;
        bcSave saveButton;

        GameObject loadTextObject;
        bcLoad loadButton;

        GameObject saveLoadTextObject;
        TMP_Text saveLoadText;

        [SetUp]
        public void SetUp()
        {
            player = new GameObject();
            stats = player.AddComponent<PlayerInformation>();
            stats.transform = player.GetComponent<Transform>();

            saveLoadTextObject = new GameObject();
            saveLoadText = saveLoadTextObject.AddComponent<TextMeshPro>();
            saveLoadTextObject.SetActive(false);
            saveLoadText.SetText("");

            saveTextObject = new GameObject();
            saveButton = saveTextObject.AddComponent<bcSave>();
            saveButton.playerInformation = stats;
            saveButton.saveLoadText = saveLoadTextObject.GetComponent<TextMeshPro>();

            loadTextObject = new GameObject();
            loadButton = loadTextObject.AddComponent<bcLoad>();
            loadButton.playerInformation = stats;
            loadButton.saveLoadText = saveLoadTextObject.GetComponent<TextMeshPro>();
        }

        // Below are tests for the SAVE text
        [Test]
        public void US49SaveLoadText_TestIfTextIsNotActiveBeforeSave()
        {
            Assert.IsFalse(saveLoadTextObject.activeSelf);
        }

        [Test]
        public void US49SaveLoadText_TestIfTextIsActiveAfterSave()
        {
            saveButton.ButtonEvent(null);
            Assert.IsTrue(saveLoadTextObject.activeSelf);
        }

        [Test]
        public void US49SaveLoadText_TestIfTextIsChangedAfterSave()
        {
            saveButton.ButtonEvent(null);

            string saveText = saveButton.saveLoadText.text;
            bool isSaveStringCorrect = saveText.Equals("Save complete");

            Assert.IsTrue(isSaveStringCorrect);
        }

        // Below are tests for the LOAD text
        [Test]
        public void US49SaveLoadText_TestIfTextIsNotActiveBeforeLoad()
        {
            Assert.IsFalse(saveLoadTextObject.activeSelf);
        }

        [Test]
        public void US49SaveLoadText_TestIfTextIsActiveAfterLoad()
        {
            loadButton.ButtonEvent(null);
            Assert.IsTrue(saveLoadTextObject.activeSelf);
        }

        [Test]
        public void US49SaveLoadText_TestIfTextIsChangedAfterLoad()
        {
            loadButton.ButtonEvent(null);

            string loadText = loadButton.saveLoadText.text;
            bool isLoadStringCorrect = loadText.Equals("Load complete");

            Assert.IsTrue(isLoadStringCorrect);
        }


    }
}
