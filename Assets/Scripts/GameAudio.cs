using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    private AudioSource audioSource;

    private GameObject[] go;
    private bool NotFirst = false;
    float volumeValue;

    void Start()
    {
        go = GameObject.FindGameObjectsWithTag("GameMusic");
        
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

    void FixedUpdate()
    {
        volumeValue = PlayerPrefs.GetFloat("Volume");
        audioSource.volume = volumeValue;
        //Debug.Log(volumeValue);
    }

    public void PlayMusic()
    {
        //if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
