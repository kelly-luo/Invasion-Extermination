using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class Volume_Test : MonoBehaviour
{
    SetVolume volumeclass;
    [SetUp]
    public void SetUp()
    {
        volumeclass = new GameObject().AddComponent<SetVolume>();
        volumeclass.Slider = Substitute.For<Slider>();
        volumeclass.mixer = Substitute.For<AudioMixer>();
    }

    [Test]
    public void Slider_Value_Does_Not_Exceed_One()
    {
        //Arrange
        float testVal = 999f;
        //Act
        volumeclass.SetLevel(testVal);
        //Assert
        float actual = volumeclass.CurrentVol;
        Assert.AreEqual(1f, actual);
    }

    [Test]
    public void Slider_Value_Does_Not_Go_Below_Lowest_Volume_Value()
    {
        //Arrange
        float testVal = -999f;
        //Act
        volumeclass.SetLevel(testVal);
        //Assert
        float actual = volumeclass.CurrentVol;
        Assert.AreEqual(0.0001f, actual);
    }

    [Test]
    public void No_Sound_When_Mute_Is_Click()
    {
        //Arrange
        volumeclass.SetLevel(1f);
        //Act
        volumeclass.SetMute(true);
        //Assert
        float actual = volumeclass.CurrentVol;
        Assert.AreEqual(0.0001f, actual);
    }

}
