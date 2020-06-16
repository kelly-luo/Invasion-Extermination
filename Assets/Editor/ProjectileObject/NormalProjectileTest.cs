using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
//This class will unit test Normal Projectile Class 
namespace Tests
{
    public class NormalProjectileTest
    {
        NormalProjectile normalProjectile;
        PlayerStateController playerStateController;
        PlayerInformation playerInfo;
        GameObject projectileObject;
        GameObject playerTarget;

        [SetUp]
        public void SetUpTest()
        {
            playerTarget = new GameObject();
            projectileObject = new GameObject();
            normalProjectile = projectileObject.AddComponent<NormalProjectile>();
            playerTarget.tag = "Player";

            playerInfo = playerTarget.AddComponent<PlayerInformation>();
            playerStateController = playerTarget.AddComponent<PlayerStateController>();
            playerInfo.Health = 100f;
        }

        //This method will test is game object is destoryed
        [Test]
        public void OnCollisionWithObstacle_DestroyedTest()
        {
            normalProjectile.OnCollisionWithObstacle();
            
            Assert.AreEqual(true, normalProjectile.IsDisposing);
        }

        [Test]
        public void OnCollisionWithPlayer_TakeDamageTest()
        {
            normalProjectile.CollisionDamage = 50f;
            normalProjectile.OnCollisionWithPlayer(playerTarget);

            Assert.AreEqual(50f, playerInfo.Health);
        }

        [TearDown]
        public void Teardown()
        {
            if(playerTarget != null)
                Object.DestroyImmediate(playerTarget);

            if (projectileObject != null)
                Object.DestroyImmediate(projectileObject);
        }
    }
}
