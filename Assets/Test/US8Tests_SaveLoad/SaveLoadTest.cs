using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace US8Tests_SaveLoad
{
    public class SaveLoadTest
    {
        private PlayerInformation playerInformation;

        [SetUp]
        public void Setup()
        {
            GameObject player = new GameObject();
            playerInformation = player.AddComponent<PlayerInformation>();

            // first initialise the player's stats
            Vector3 position1 = new Vector3(1f, -7f, 2.55f);
            playerInformation.transform.position = position1;
            playerInformation.Health = 97.2f;
            playerInformation.Level = 10;
            playerInformation.Score = 3501;
            playerInformation.Money = 999999;
        }

        // Test if the save file exists in the location 
        [Test]
        public void TestSavePlayer_SaveFileExistsAfterSaving()
        {
            SaveSystem.SavePlayer(playerInformation);

            // Path.GetFileName() will return a null if not found
            Assert.IsNotNull(Path.GetFileName(Application.persistentDataPath + "/playerSaveFile"));
        }

        // Test if the loaded character has correct position (x,y,z)
        [Test]
        public void TestLoadPlayer_LoadedPositionFromFileIsCorrect()
        {
            SaveSystem.LoadPlayer();

            //check for x position
            Assert.AreEqual(1f, playerInformation.transform.position[0]);

            //check for y position
            Assert.AreEqual(-7f, playerInformation.transform.position[1]);

            //check for z position
            Assert.AreEqual(2.55f, playerInformation.transform.position[2]);
        }

        // Test if the loaded character has the correct health points
        [Test]
        public void TestLoadPlayer_LoadedHealthFromFileIsCorrect()
        {
            SaveSystem.LoadPlayer();

            //check for health points
            Assert.AreEqual(97.2f, playerInformation.Health);

        }

        // Test if the loaded character has the correct health points
        [Test]
        public void TestLoadPlayer_LoadedLevelFromFileIsCorrect()
        {
            SaveSystem.LoadPlayer();

            //check for health points
            Assert.AreEqual(10, playerInformation.Level);

        }

        // Test if the loaded character has the correct health points
        [Test]
        public void TestLoadPlayer_LoadedScoreFromFileIsCorrect()
        {
            SaveSystem.LoadPlayer();

            //check for health points
            Assert.AreEqual(3501, playerInformation.Score);

        }

        // Test if the loaded character has the correct health points
        [Test]
        public void TestLoadPlayer_LoadedMoneyFromFileIsCorrect()
        {
            SaveSystem.LoadPlayer();

            //check for health points
            Assert.AreEqual(999999, playerInformation.Money);

        }
    }
}
