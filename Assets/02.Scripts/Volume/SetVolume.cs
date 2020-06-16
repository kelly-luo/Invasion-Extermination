using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Toggle mute_toggle;

    private float currentVol = 1f;
    public float CurrentVol
    {
        get { return currentVol; }
        set { currentVol = value; }
    }
    private Slider slider;
    public Slider Slider
    {
        get { return slider; }
        set { slider = value; }
    }
    private bool mute = false;
    public bool Mute
    {
        get { return mute; }
    }
    private const float ZEROVAL = 0.0001f;
    private const string VOLNAME = "GameVol";


    void Start()
    {
        Intialize();
    }

    private void Intialize()
    {
        mute_toggle.onValueChanged.AddListener(delegate
        {
            SetMute(mute_toggle.isOn);
        });
        slider = GetComponent<Slider>();
        SetLevel(currentVol);
    }

    public void SetLevel (float SliderVal)
    {
        if (SliderVal > 1f) SliderVal = 1f;
        else if (SliderVal < 0.0001f) SliderVal = 0.0001f;

        currentVol = SliderVal;
        if (slider != null) slider.value = currentVol;
        if (!mute && mixer != null) mixer.SetFloat(VOLNAME, VolumeLog(currentVol));
    }
    public void SetMute(bool mute)
    {
        if (mute)
        {
            SetLevel(ZEROVAL);
            this.mute = mute;
        }
        else
        {
            this.mute = mute;
            SetLevel(slider.value);
        }
    }

    private float VolumeLog(float SliderVal)
    {
        return Mathf.Log10(SliderVal) * 20;
    }

    


}
