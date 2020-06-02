using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MoneyTest
    {
        GameObject money;
        GameObject player;
        Money moneyClass;
        PlayerInformation playerInfo;

        [SetUp]
        public void SetUpTest()
        {
            money = new GameObject();
            moneyClass = money.AddComponent<Money>();

            player = new GameObject();
            playerInfo = player.AddComponent<PlayerInformation>();

            playerInfo.Money = 0;
        }

        [Test]
        public void Money_InteractionWithPlayerTest()
        {
            moneyClass.MoneyAmount = 10;
            moneyClass.OnCollisionWithPlayer(player);

            Assert.AreEqual(10, playerInfo.Money);
        }
    }
}
