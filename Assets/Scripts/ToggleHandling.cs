using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandling : MonoBehaviour
{
    Toggle musicToggle;

    void Start()
    {
        musicToggle = GetComponent<Toggle>();
    }

    void Update()
    {
        musicToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(musicToggle);
        });
    }

    void ToggleValueChanged(Toggle change)
    {
        if (musicToggle.isOn == true)
        {
            SetVolume(0.5f);
        }
        else
        {
            SetVolume(0.0f);
        }
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
    }
}
