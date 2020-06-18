using NUnit.Framework;
using NSubstitute;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RoundsTest : MonoBehaviour
{
    GameManager gameManager;
    UIManager uiManager;
    RoundPopUp roundpopup;

    [SetUp]
    public void SetUp()
    {
        gameManager = new GameObject().AddComponent<GameManager>();
        uiManager = new GameObject().AddComponent<UIManager>();
        roundpopup = new GameObject().AddComponent<RoundPopUp>();

        uiManager.roundPopUp = roundpopup;
        uiManager.gameManager = gameManager;

        uiManager.roundView = Substitute.For<TMP_Text>();
        uiManager.reqScoreView = Substitute.For<TMP_Text>();
        uiManager.scoreView = Substitute.For<TMP_Text>();
        uiManager.scoreProgress = Substitute.For<Slider>();

        uiManager.SetReqScore(gameManager.RequiredScore);
    }

    [Test]
    public void When_Player_Score_Is_Greater_Than_Req_Score()
    {
        //Arrange
        int playerScore = gameManager.RequiredScore + 10;
        //Act
        uiManager.SetScore(playerScore);
        //Assert
        Assert.AreEqual(true, roundpopup.playing);
    }

    [Test]
    public void After_Player_Score_Is_Equal_To_Require_Score_Then_Require_Score_Increases()
    {
        //Arrange
        int playerScore = gameManager.RequiredScore + 10;
        int originalRequiredScore = gameManager.RequiredScore;
        //Act
        uiManager.SetScore(playerScore);
        //Assert
        bool actual = originalRequiredScore < gameManager.RequiredScore;
        Assert.AreEqual(true, actual);
    }

    [Test]
    public void Max_Aliens_Increases_After_ClearRound()
    {
        //Arrange
        int playerScore = gameManager.RequiredScore + 10;
        int originalMaxAlien = gameManager.maxAlien;
        //Act
        uiManager.SetScore(playerScore);
        //Assert
        bool actual = originalMaxAlien < gameManager.maxAlien;
        Assert.AreEqual(true, actual);
    }

    [Test]
    public void Max_Humans_Increases_After_ClearRound()
    {
        //Arrange
        int playerScore = gameManager.RequiredScore + 10;
        int originalMaxHumans = gameManager.maxHuman;
        //Act
        uiManager.SetScore(playerScore);
        //Assert
        bool actual = originalMaxHumans < gameManager.maxHuman;
        Assert.AreEqual(true, actual);
    }


}
