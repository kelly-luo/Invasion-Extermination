using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GainPoints
    {
        private GameObject player;
        //private WeaponAK74 playerGun;
        private PlayerInformation playerInformation;

        private GameObject monster;
        private MonsterController monsterController;
        //private WeaponM1911 enemyGun;

        [SetUp]
        public void Setup()
        {
            // create a GameObject to attach the PlayerInformation component
            player = new GameObject();
            //playerGun = player.AddComponent<WeaponAK74>();
            playerInformation = player.AddComponent<PlayerInformation>();

            Object playerPrefab = AssetDatabase.LoadAssetAtPath("03.Prefabs/EnemyPrefab/AlienWithPistol.prefab", typeof(Object));

            //Instantiate prefab if it exists
            if (playerPrefab != null)
            {
                PrefabUtility.InstantiatePrefab(playerPrefab);
            }
            //monster = PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("03.Prefabs/EnemyPrefab/AlienWithPistol"));
            //monster = Resources.Load(Path, typeof(GameObject)) as GameObject;
            //monsterController = monster.AddComponent<MonsterController>();
            //enemyGun = monster.AddComponent<WeaponM1911>();


        }

        // Test to gain points when the player kills enemy
        [UnityTest]
        public IEnumerator GainPointsTest_GainPointsAfterKillMonster()
        {
            monsterController.Stats.Health = 20f;
            monsterController.TakeDamage(20f);

            Assert.AreEqual(10, playerInformation.Score);

            yield return null;
        }

        // Test for player not gaining points when enemy has not died
        [UnityTest]
        public IEnumerator GainPointsTest_NotGainPointsWhenMonsterNotDead()
        {
            monsterController.Stats.Health = 100f;
            monsterController.TakeDamage(20f);

            Assert.AreEqual(0, playerInformation.Score);

            yield return null;
        }
    }
}
