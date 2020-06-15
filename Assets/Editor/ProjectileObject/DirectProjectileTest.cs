using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
//This class will unit test Direct Projectile Class 
namespace Tests
{
    public class DirectProjectileTest
    {
        DirectProjectile directProjectile;
        PlayerStateController playerStateController;
        PlayerInformation playerInfo;
        GameObject projectileObject;
        GameObject playerTarget;

        [SetUp]
        public void SetUpTest()
        {
            playerTarget = new GameObject();
            projectileObject = new GameObject();
            directProjectile = projectileObject.AddComponent<DirectProjectile>();
            playerTarget.tag = "Player";

            playerInfo = playerTarget.AddComponent<PlayerInformation>();
            playerStateController = playerTarget.AddComponent<PlayerStateController>();
            playerInfo.Health = 100f;
        }

        //This type of projectile should not destoryed on hit with obstacle but after throwing is done
        [Test]
        public void OnCollisionWithObstacle_DestroyedTest()
        {
            directProjectile.AfterThrow();

            Assert.AreEqual(true, directProjectile.IsDisposing);
        }

        [Test]
        public void OnCollisionWithPlayer_TakeDamageTest()
        {
            directProjectile.CollisionDamage = 50f;
            directProjectile.OnCollisionWithPlayer(playerTarget);

            Assert.AreEqual(50f, playerInfo.Health);
        }

        [TearDown]
        public void Teardown()
        {
            if (playerTarget != null)
                Object.DestroyImmediate(playerTarget);

            if (projectileObject != null)
                Object.DestroyImmediate(projectileObject);
        }
    }
}
