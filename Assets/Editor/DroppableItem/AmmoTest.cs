//
// US49SaveLoadTextTest
// ~~~~~~~~~~~~~~~~~~~~
// This test class is used to test user story 53: As a player, I want enemies to drop ammo so that I can keep shooting without worrying too much about conserving ammo.
// 
// AUT University - 2020 - Kelly Luo
// 
// Revision History
// ~~~~~~~~~~~~~~~~
// 14.06.2020 Creation date
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
    public class AmmoTest
    {
        GameObject ammo;
        GameObject player;
        Ammo ammoClass;
        PlayerInformation playerInfo;

        [SetUp]
        public void SetUpTest()
        {
            ammo = new GameObject();
            ammoClass = ammo.AddComponent<Ammo>();

            player = new GameObject();
            playerInfo = player.AddComponent<PlayerInformation>();

            playerInfo.Ammo = 0;
        }

        [Test]
        public void Money_InteractionWithPlayerTest()
        {
            ammoClass.AmmoAmount = 10;
            ammoClass.OnCollisionWithPlayer(player);

            Assert.AreEqual(10, playerInfo.Ammo);
        }
    }
}
