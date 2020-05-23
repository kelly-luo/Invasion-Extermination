using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RespawnTest
{
    public class RespawnTest
    {
        Spawn spawn;

        [SetUp]
        public void SetUpTest()
        {
            spawn = new Spawn();
        }

        [Test]
        public void check_for_SelectSpawnPoint()
        {
            var unityService = Substitute.For<IUnityServiceManager>();
            unityService.UnityRandomRange(0, spawn.SpawnPoints.Count).Returns(0);
            spawn.UnityService = unityService;
            Transform testTransform = spawn.SelectSpawnPoint();

            //Assert
            Assert.AreEqual(new Vector3(-7.1f, 0f, 5.86f), testTransform.localPosition);
        }

        [Test]
        public void check_for_SelectSpawnPoint_2()
        {
            var unityService = Substitute.For<IUnityServiceManager>();
            unityService.UnityRandomRange(0, spawn.SpawnPoints.Count).Returns(1);
            spawn.UnityService = unityService;
            Transform testTransform = spawn.SelectSpawnPoint();

            //Assert
            Assert.AreEqual(new Vector3(-26.05f, 0f, 5.86f), testTransform.localPosition);
        }

    }
}
