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
            target.AddComponent<Rigidbody>();
            target.AddComponent<CapsuleCollider>();
            target.transform.position = new Vector3(0f, 0f, 0f);
            playerTranslate = new PlayerTranslate(target.transform);

            var unitService = Substitute.For<IUnityServiceManager>();
            unitService.DeltaTime.Returns(1);

            playerTranslate.UnityService = unitService;
        }

        [Test]
        public void TranslateCharacter_MovingTest()
        {
            //Since checking ground method should always make reverse the IsJumping value if player is not on the ground.
            playerTranslate.TranslateCharacter(new Vector3(1f, 0f, 0f));

            var ExpectedValue = new Vector3(playerTranslate.Speed, 0f,0f);
            //Check is DesiredPostion is same as expected
            Assert.AreEqual(ExpectedValue, playerTranslate.DesiredPosition);
        }
        [Test]
        public void TranslateCharacterOnGround_MovingTest()
        {
            //Since checking ground method should always make reverse the IsJumping value if player is not on the ground.
            playerTranslate.TranslateCharacterOnGround(true,new Vector3(1f, 0f, 0f));

            var ExpectedValue = new Vector3(playerTranslate.SpeedDuringJump, 0f, 0f);
            //Check is DesiredPostion is same as expected
            Assert.AreEqual(ExpectedValue, playerTranslate.DesiredPosition);
        }

        [Test] 
        public void TranslateCharacter_JumpingTest()
        {
            //CheckingPlayerIsOnGround is false
            bool IsJumpSuccess = playerTranslate.JumpCharacter(false);
            

            Assert.AreEqual(true, IsJumpSuccess);
        }
        [Test]
        public void TranslateCharacter_CheckCharacterIsOnGroundTest()
        {
            bool IsCharacterOnGround = playerTranslate.CheckCharacterIsOnGround();

            Assert.AreEqual(false, IsCharacterOnGround);
        }
        

    }
}
