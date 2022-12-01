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
    public TMP_InputField playerOneName;
    public TMP_InputField playerTwoName;
    public bool fieldCheck = false;
    public Button continueButton;

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

    public void BeginGame()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().StopMusic();
        SceneManager.LoadScene(gameScene);
    }
    /*
    void Update()
    {
        if (playerOneName.GetComponent<TMP_InputField>().text.Length == 3)
        {
            fieldCheck = true;
        }

        if (fieldCheck == true && continueButton.interactable == false)
        {
            continueButton.interactable = true;
        }
    }*/
}