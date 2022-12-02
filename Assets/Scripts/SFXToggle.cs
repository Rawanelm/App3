using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXToggle : MonoBehaviour
{
    Toggle soundToggle;

    void Start()
    {
        soundToggle = GetComponent<Toggle>();
    }

    void Update()
    {
        soundToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(soundToggle);
        });

        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            soundToggle.isOn = false;
        }
        else
        {
            soundToggle.isOn = true;
        }
    }

    void ToggleValueChanged(Toggle change)
    {
        if (soundToggle.isOn == true)
        {
            SetSFXVolume(0.5f);
            SoundToggleOn();
        }
        else
        {
            SetSFXVolume(0.0f);
            SoundToggleOff();
        }
    }

    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SoundToggleOn()
    {
        PlayerPrefs.SetInt("SoundOn", 1);
    }

    public void SoundToggleOff()
    {
        PlayerPrefs.SetInt("SoundOn", 0);
    }
}

