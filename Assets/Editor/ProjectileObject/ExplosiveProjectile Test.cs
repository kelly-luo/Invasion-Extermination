using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ExplosiveProjectileTest
    {
        ExplosiveProjectile explosiveProjectile;
        PlayerStateController playerStateController;
        PlayerInformation playerInfo;
        GameObject projectileObject;
        GameObject playerTarget;

        [SetUp]
        public void SetUpTest()
        {
            playerTarget = new GameObject();
            projectileObject = new GameObject();
            explosiveProjectile = projectileObject.AddComponent<ExplosiveProjectile>();
            playerTarget.tag = "Player";

            playerInfo = playerTarget.AddComponent<PlayerInformation>();
            playerStateController = playerTarget.AddComponent<PlayerStateController>();
            playerTarget.AddComponent<Rigidbody>();
            playerTarget.AddComponent<CapsuleCollider>();
            playerInfo.Health = 100f;
        }

        //This type of projectile should not destoryed on hit with obstacle but after throwing is done
        [Test]
        public void OnCollisionWithObstacle_DestroyedTest()
        {
            explosiveProjectile.AfterThrow();

            Assert.AreEqual(true, explosiveProjectile.IsDisposing);
        }
        //see the player take damage on collosion
        [Test]
        public void OnCollisionWithPlayer_TakeDamageTest()
        {
            explosiveProjectile.CollisionDamage = 5f;
            explosiveProjectile.OnCollisionWithPlayer(playerTarget);

            Assert.AreEqual(true, playerInfo.Health < 100);
        }
        //test player take damage after explosion
        [Test]
        public void OnCollisionWithPlayer_ExplosionTest()
        {
            playerTarget.transform.position = new Vector3(1f, 0f, 1f);
            projectileObject.transform.position = new Vector3(0f,0f,0f);
            explosiveProjectile.ExplosionDamage = 5f;
            explosiveProjectile.ProjectileExplosion();

            Assert.AreEqual(true, playerInfo.Health < 100);
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
