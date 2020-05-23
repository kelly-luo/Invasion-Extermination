using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameRoundTest
    {

        private GameObject player;

        [SetUp]
        public void Setup()
        {
            player = new GameObject();
        }

        // Every unit tests it will tear down the object
        [TearDown]
        public void Teardown()
        {
            //Dispose the player object
            Object.Destroy(player);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Test_RoundSuccessfullyLoaded()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
