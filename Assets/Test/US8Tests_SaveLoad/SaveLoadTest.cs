//using System.Collections;
//using System.IO;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;

//namespace US8Tests_SaveLoad
//{
//    public class SaveLoadTest
//    {
//        private PlayerTranslate playerTranslate;
//        private PlayerStateController playerStateController;
//        private PlayerStats stats;

//        [SetUp]
//        public void Setup()
//        {
//            //GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("03.Prefabs/Characters/TestPlayer"));

//            Vector3 position1 = new Vector3(1f, 0f, 0f);
//            playerStateController.Transform.position = position1;

//        }

//        //[TearDown]
//        //public void Teardown()
//        //{
//        //    Object.Destroy(game.gameObject);
//        //}

//        // Test if the save file exists in the location 
//        [Test]
//        public void TestSaveFileExistsAfterSaving()
//        {
//            SaveSystem.SavePlayer(playerStateController);

//            // Path.GetFileName() will return a null if not found
//            Assert.IsNotNull(Path.GetFileName(Application.persistentDataPath + "/playerSaveFile"));
//        }

//        // Test if the loaded character has correct position (x,y,z)
//        [Test]
//        public void TestLoadedPositionFromFileIsCorrect()
//        {
            
//        }

//        // Test if the loaded character has the correct health points


//        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
//        // `yield return null;` to skip a frame.
//        [UnityTest]
//        public IEnumerator TestWithEnumeratorPasses()
//        {
//            // Use the Assert class to test conditions.
//            // Use yield to skip a frame.
//            yield return null;
//        }


//        [UnityTest]
//        public IEnumerator AsteroidsMoveDown()
//        {
//            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
//            float initialYPos = asteroid.transform.position.y;
//            yield return new WaitForSeconds(0.1f);

//            Assert.Less(asteroid.transform.position.y, initialYPos);
//        }





//    }
//}
