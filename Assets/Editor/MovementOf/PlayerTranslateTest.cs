using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTranslateTest
    {
        PlayerTranslate playerTranslate;
        GameObject target;

        [SetUp]
        public void SetupTest()
        {
            target = new GameObject();
            playerTranslate = new PlayerTranslate(target.transform);

            var unitService = Substitute.For<IUnityServiceManager>();
            unitService.DeltaTime.Returns(1);

            playerTranslate.UnityService = unitService;
        }

        [Test]
        public void TranslateCharacter_MovingTest()
        {
            this.SetupTest();
            
            playerTranslate.TranslateCharacter(new Vector3(1f, 0f, 0f));

            var ExpectedValue = new Vector3(1.5f, 0f,0f);

            Assert.AreEqual(ExpectedValue, target.transform.position);
        }
    }
}
