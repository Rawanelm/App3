using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public string characterScene;
    public string settingsScene;
    public string gameScene;

    void Start()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().PlayMusic();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(characterScene);
    }

    public void QuitGame()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().StopMusic();
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void Settings()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().StopMusic();
        SceneManager.LoadScene(settingsScene);
    }

    

}