using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace US8Tests_SaveLoad
{
    public class SaveLoadTest
    {
        private PlayerInformation playerInformation;
        private GameObject player;
        private TestableSaveSystem saveSystem;
        private ISaveSystem iSaveSystem;

        [SetUp]
        public void Setup()
        {
            // create a GameObject to attach the PlayerInformation component
            player = new GameObject();
            playerInformation = player.AddComponent<PlayerInformation>();

            saveSystem = new TestableSaveSystem();

            // first initialise the player's stats and position
            Vector3 position1 = new Vector3(1f, -7f, 2.55f);
            playerInformation.transform.position = position1;
            playerInformation.Health = 97.2f;
            playerInformation.Level = 10;
            playerInformation.Score = 3501;
            playerInformation.Money = 999999;
        }

        [TearDown]
        public void Teardown()
        {
            //Dispose the player object
            Object.Destroy(player);
        }

        // Test if the save file exists in the location 
        [Test]
        public void TestSavePlayer_SaveFileExistsAfterSaving()
        {
            saveSystem.Save(playerInformation);

            // Path.GetFileName() will return a null if not found
            Assert.IsNotNull(Path.GetFileName(Application.persistentDataPath + "/playerSaveFile"));
        }

        // Test if the loaded character has correct position (x,y,z)
        [Test]
        public void TestLoadPlayer_LoadedPositionFromFileIsCorrect()
        {
            saveSystem.Load();

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
            saveSystem.Load();

            //check for health points
            Assert.AreEqual(97.2f, playerInformation.Health);

        }

        // Test if the loaded character has the correct health points
        [Test]
        public void TestLoadPlayer_LoadedLevelFromFileIsCorrect()
        {
            saveSystem.Load();

            //check for health points
            Assert.AreEqual(10, playerInformation.Level);

        }

        // Test if the loaded character has the correct health points
        [Test]
        public void TestLoadPlayer_LoadedScoreFromFileIsCorrect()
        {
            saveSystem.Load();

            //check for health points
            Assert.AreEqual(3501, playerInformation.Score);

        }

        // Test if the loaded character has the correct health points
        [Test]
        public void TestLoadPlayer_LoadedMoneyFromFileIsCorrect()
        {
            saveSystem.Load();

            //check for health points
            Assert.AreEqual(999999, playerInformation.Money);

        }

        // Test if the saving new player Health and loading is correct
        [Test]
        public void TestLoadPlayer_ChangeHealthAndLoadingFromOldSaveIsCorrect()
        {
            playerInformation.Health = 97.2f;
            saveSystem.Save(playerInformation);

            // simulate player losing health
            playerInformation.Health = 9.271f;

            // simulate the player re-loading from previous save
            saveSystem.Load();

            //check for health points is still same from previous save
            Assert.AreEqual(97.2f, playerInformation.Health);

        }
    }
}
