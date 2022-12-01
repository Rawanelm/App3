using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public string mainMenu;
    public GameObject pauseMenu;
    public bool isPaused;
    public string settingsScene;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
            if (isPaused)
            {
                Resume();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void ToMain()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void Settings()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().StopMusic();
        SceneManager.LoadScene(settingsScene);
    }

    public void QuitGame()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().StopMusic();
        Application.Quit();
        Debug.Log("Quitting");
    }
}
