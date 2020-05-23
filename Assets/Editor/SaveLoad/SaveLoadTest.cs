using System.Collections;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace US8Tests_SaveLoad
{
    public class SaveLoadingTest
    {
        private PlayerInformation playerInformation;
        private GameObject player;

        [SetUp]
        public void Setup()
        {
            // create a GameObject to attach the PlayerInformation component
            player = new GameObject();
            playerInformation = player.AddComponent<PlayerInformation>();

            // first initialise the player's stats and position
            Vector3 position1 = new Vector3(1f, -7f, 2.55f);
            playerInformation.transform.position = position1;
            playerInformation.Health = 97.2f;
            playerInformation.Level = 10;
            playerInformation.Score = 3501;
            playerInformation.Money = 999999;
        }

        // Every unit tests it will tear down the object
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
            playerInformation.SavePlayer();

            // Path.GetFileName() will return a null if not found
            Assert.IsNotNull(Path.GetFileName(Application.persistentDataPath + "/playerSaveFile"));
        }

        // Test if the loaded character has correct position (x,y,z)
        [UnityTest]
        public IEnumerator TestLoadPlayer_LoadedPositionFromFileIsCorrect()
        {
            playerInformation.LoadPlayer();

            //check for x position
            Assert.AreEqual(1f, playerInformation.transform.position[0]);

            //check for y position
            Assert.AreEqual(-7f, playerInformation.transform.position[1]);

            //check for z position
            Assert.AreEqual(2.55f, playerInformation.transform.position[2]);

            yield return null;

        }

        // Test if the loaded character has the correct health points
        [UnityTest]
        public IEnumerator TestLoadPlayer_LoadedHealthFromFileIsCorrect()
        {
            playerInformation.LoadPlayer();

            //check for health points
            Assert.AreEqual(97.2f, playerInformation.Health);

            yield return null;

        }

        // Test if the loaded character has the correct health points
        [UnityTest]
        public IEnumerator TestLoadPlayer_LoadedLevelFromFileIsCorrect()
        {
            playerInformation.LoadPlayer();

            //check for health points
            Assert.AreEqual(10, playerInformation.Level);

            yield return null;
        }

        // Test if the loaded character has the correct health points
        [UnityTest]
        public IEnumerator TestLoadPlayer_LoadedScoreFromFileIsCorrect()
        {
            playerInformation.LoadPlayer();

            //check for health points
            Assert.AreEqual(3501, playerInformation.Score);

            yield return null;
        }

        // Test if the loaded character has the correct health points
        [UnityTest]
        public IEnumerator TestLoadPlayer_LoadedMoneyFromFileIsCorrect()
        {
            playerInformation.LoadPlayer();

            //check for health points
            Assert.AreEqual(999999, playerInformation.Money);

            yield return null;
        }

        // Test if the saving new player Health and loading is correct
        [UnityTest]
        public IEnumerator TestLoadPlayer_ChangeHealthAndLoadingFromOldSaveIsCorrect()
        {
            playerInformation.SavePlayer();

            // simulate player losing health
            playerInformation.Health = 9.271f;

            // simulate the player re-loading from previous save
            playerInformation.LoadPlayer();

            //check for health points is still same from previous save
            Assert.AreEqual(97.2f, playerInformation.Health);

            yield return null;

        }

        // Test if the saving new player Health and loading is correct
        [UnityTest]
        public IEnumerator TestLoadPlayer_ChangePositionAndLoadingFromOldSaveIsCorrect()
        {
            playerInformation.SavePlayer();

            // simulate player losing health
            Vector3 position1 = new Vector3(-1.56f, 230f, -89f);
            playerInformation.transform.position = position1;

            // simulate the player re-loading from previous save
            playerInformation.LoadPlayer();

            //check for position is still same from previous save
            Assert.AreEqual(1f, playerInformation.transform.position[0]);
            Assert.AreEqual(-7f, playerInformation.transform.position[1]);
            Assert.AreEqual(2.55f, playerInformation.transform.position[2]);

            yield return null;

        }
    }
}
