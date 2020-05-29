using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
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

            SetUpNewMob();
        }

        public void SetUpNewMob()
        {
            monsterController = prefab.AddComponent<MonsterController>();
            monsterController.Stats = new MonsterStats();
            monsterController.playerInformation = playerInformation;
            monsterController.Animator = prefab.GetComponent<Animator>();
            monsterController.UnityService = UnityServiceManager.Instance;
            monsterController.Agent = prefab.GetComponent<NavMeshAgent>();
            monsterController.Agent.isStopped = true;
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
        [Test]
        public void GainPointsTest_NotGainPointsWhenMonsterNotDead()
        {
            monsterController.Stats.Health = 100f;
            monsterController.TakeDamage(20f);

            Assert.AreEqual(10, playerInformation.Score);

            //yield return null;
        }
    }
}
