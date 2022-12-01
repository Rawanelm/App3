using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject[] go;
    private bool NotFirst = false;

    void Awake()
    {
        go = GameObject.FindGameObjectsWithTag("MenuMusic");
        //allAudioSources = FindObjectsOfType(AudioSource);

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
}
