using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandling : MonoBehaviour
{
    Toggle musicToggle;
    bool musicBool;

    void Start()
    {
        musicToggle = GetComponent<Toggle>();
    }

    void Update()
    {
        musicToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(musicToggle);
        });

        if(PlayerPrefs.GetInt("MusicOn") == 0)
        {
            musicToggle.isOn = false;
        }
        else
        {
            musicToggle.isOn = true;
        }
    }

    void ToggleValueChanged(Toggle change)
    {
        if (musicToggle.isOn == true)
        {
            SetVolume(0.5f);
            MusicToggleOn();
        }
        else
        {
            SetVolume(0.0f);
            MusicToggleOff();
        }
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void MusicToggleOn()
    {
        PlayerPrefs.SetInt("MusicOn", 1);
    }

    public void MusicToggleOff()
    {
        PlayerPrefs.SetInt("MusicOn", 0);
    }
}
