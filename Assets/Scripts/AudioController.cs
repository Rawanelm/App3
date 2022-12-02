using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject[] go;
    private bool NotFirst = false;
    private float musicVolume = 0.5f;
    public Slider m_slider;

    void Start()
    {
        go = GameObject.FindGameObjectsWithTag("MenuMusic");

        foreach (GameObject thing in go)
        {
            if (thing.scene.buildIndex == -1)
            {
                NotFirst = true;
            }
        }

        if (NotFirst == true)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetGameVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("Volume", volume);
    }

    void Update()
    {
        audioSource.volume = musicVolume;
        if (m_slider != null && PlayerPrefs.HasKey("Volume"))
        {
            float wantedVolume = PlayerPrefs.GetFloat("Volume", 1f);
            m_slider.value = wantedVolume;
            AudioListener.volume = wantedVolume;

            m_slider.onValueChanged.AddListener(delegate { SetGameVolume(m_slider.value); });

        }
    }
}
