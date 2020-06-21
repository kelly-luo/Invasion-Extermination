//
// US49SaveLoadTextTest
// ~~~~~~~~~~~~~~~~~~~~
// This test class is used to test user story 31: As a player I want a way to regain health from killing enemies so that I can gain health as I fight.
// 
// AUT University - 2020 - Kelly Luo
// 
// Revision History
// ~~~~~~~~~~~~~~~~
// 18.06.2020 Creation date
//

//
// NUnit support packages
// ~~~~~~~~~~~~~~~~~~~~~
using NUnit.Framework;
//
// Unity support packages
// ~~~~~~~~~~~~~~~~~~~~~
using UnityEngine;

namespace Tests
{
    public class HealthTest
    {
        GameObject health;
        GameObject player;
        HealthDrop healthClass;
        PlayerInformation playerInfo;

        [SetUp]
        public void SetUpTest()
        {
            health = new GameObject();
            healthClass = health.AddComponent<HealthDrop>();

            player = new GameObject();
            playerInfo = player.AddComponent<PlayerInformation>();

            playerInfo.Health = 0;
        }

        [Test]
        public void Health_InteractionWithPlayerTest()
        {
            healthClass.HealthAmount = 10;
            healthClass.OnCollisionWithPlayer(player);

            Assert.AreEqual(10, playerInfo.Health);
        }
    }
}
