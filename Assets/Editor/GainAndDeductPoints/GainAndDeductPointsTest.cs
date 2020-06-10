using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Tests
{
    /**
     * This test class is used for testing US17:
     * As a player I want to gain points after killing a Enemy so that it adds to my point system (Sprint 1) which is the first half
     * 
     * and US43: As a player I want points to be taken away when I kill humans so that I'm incentivized to not kill humans,
     * and instead focus on killing the aliens. (Sprint 2) which is the second half
     * 
     * Player gains 10 points from killing a monster and deducted 20 points from killing a human.
     * 
     * It uses the same SetUp() function and so it was decided to add to the same test class.
     **/
    public class GainAndDeductPointsTest
    {
        private GameObject player;
        private PlayerInformation playerInformation;

        private GameObject monsterPrefab;
        private MonsterController monsterController;

        private GameObject humanPrefab;
        private MonsterController humanController;

        [SetUp]
        public void SetUp()
        {
            // create a GameObject to attach the PlayerInformation component
            player = new GameObject();
            playerInformation = player.AddComponent<PlayerInformation>();

            var monsterPrefabVar = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/03.Prefabs/EnemyPrefab/AlienWithPistol.prefab");
            var humanPrefabVar = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/03.Prefabs/EnemyPrefab/Human.prefab");

            //Instantiate prefab if it exists
            if (monsterPrefabVar != null)
            {
                monsterPrefab = (GameObject)PrefabUtility.InstantiatePrefab(monsterPrefabVar);
                humanPrefab = (GameObject)PrefabUtility.InstantiatePrefab(humanPrefabVar);
            }

            SetUpMonsterAndHuman();
        }

        public void SetUpMonsterAndHuman()
        {
            // Set up for monster object
            monsterController = monsterPrefab.AddComponent<MonsterController>();
            monsterController.Stats = new MonsterStats();
            monsterController.playerInformation = playerInformation;
            monsterController.Animator = monsterPrefab.GetComponent<Animator>();
            monsterController.UnityService = UnityServiceManager.Instance;
            monsterController.Agent = monsterPrefab.GetComponent<NavMeshAgent>();

            // Set up for human object
            humanController = humanPrefab.AddComponent<MonsterController>();
            humanController.Stats = new MonsterStats();
            humanController.playerInformation = playerInformation;
            humanController.Animator = humanPrefab.GetComponent<Animator>();
            humanController.UnityService = UnityServiceManager.Instance;
            humanController.Agent = humanPrefab.GetComponent<NavMeshAgent>();
        }

        // Tests below are for US17 points gain for killing monster (Sprint 2) -----------------------------------
        
        // Test to gain points when the player kills enemy
        [Test]
        public void GainPointsTest_GainPointsAfterKillMonster()
        {
            monsterController.Stats.Health = 20f;
            monsterController.TakeDamage(20f);

            Assert.AreEqual(10, playerInformation.Score);

            playerInformation.Score = 0; // reset score for each test
        }

        // Test for player not gaining points when enemy has not died
        [Test]
        public void GainPointsTest_NotGainPointsWhenMonsterNotDead()
        {
            monsterController.Stats.Health = 100f;
            monsterController.TakeDamage(20f);

            Assert.AreEqual(0, playerInformation.Score);

            playerInformation.Score = 0; // reset score for each test
        }


        // Tests below are for US43 points deduction for killing human (Sprint 2) -----------------------------------

        // Test for points being deducted if a human is killed
        [Test]
        public void DeductPointsTest_PointsDeductedWhenHumanIsKilled()
        {
            playerInformation.Score = 50; // Player has 50 score points
            humanController.Stats.Health = 20f;
            humanController.TakeDamage(20f);

            Assert.AreEqual(30, playerInformation.Score);

            playerInformation.Score = 0; // reset score for each test
        }

        // Test for points should be deducted should not go negative if a human is killed
        [Test]
        public void DeductPointsTest_PointsDeductedBelowLineWhenHumanIsKilled()
        {
            playerInformation.Score = 15; // Player has 15 score points
            humanController.Stats.Health = 20f;
            humanController.TakeDamage(20f);

            Assert.AreEqual(0, playerInformation.Score);

            playerInformation.Score = 0; // reset score for each test
        }

        [Test]
        public void DeductPointsTest_PointsNotDeductedWhenHumanNotDead()
        {
            playerInformation.Score = 50; // Player has 50 score points
            humanController.Stats.Health = 100f;
            humanController.TakeDamage(20f);

            Assert.AreEqual(50, playerInformation.Score);

            playerInformation.Score = 0; // reset score for each test
        }

    }
}
