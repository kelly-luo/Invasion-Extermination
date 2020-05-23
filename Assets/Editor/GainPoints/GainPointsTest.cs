﻿using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GainPoints
    {
        private GameObject player;
        private PlayerInformation playerInformation;

        private GameObject prefab;
        private MonsterController monsterController;

        [SetUp]
        public void Setup()
        {
            // create a GameObject to attach the PlayerInformation component
            player = new GameObject();
            playerInformation = player.AddComponent<PlayerInformation>();

            var playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/03.Prefabs/EnemyPrefab/AlienWithPistol.prefab");

            //Instantiate prefab if it exists
            if (playerPrefab != null)
            {
                prefab = (GameObject)PrefabUtility.InstantiatePrefab(playerPrefab);
            }

            monsterController = prefab.AddComponent<MonsterController>();
            monsterController.Stats = new MonsterStats();
            monsterController.playerInformation = playerInformation;
            monsterController.Animator = prefab.GetComponent<Animator>();
            monsterController.UnityService = UnityServiceManager.Instance;

        }

        // Test to gain points when the player kills enemy
        [Test]
        public void GainPointsTest_GainPointsAfterKillMonster()
        {
            monsterController.Stats.Health = 20f;
            monsterController.TakeDamage(20f);

            Assert.AreEqual(10, playerInformation.Score);
        }

        // Test for player not gaining points when enemy has not died
        [UnityTest]
        public void PointsTest_NotGainPointsWhenMonsterNotDead()
        {
            monsterController.Stats.Health = 100f;
            monsterController.TakeDamage(20f);

            Assert.AreEqual(10, playerInformation.Score);

        }
    }
}
