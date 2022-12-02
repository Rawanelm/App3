using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public string mainMenu;
    public GameObject pauseMenu;
    public bool isPaused;

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
                Time.timeScale = 0f;
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
        GameObject.FindGameObjectWithTag("GameMusic").GetComponent<GameAudio>().StopMusic();
    }

    public void QuitGame()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().StopMusic();
        Application.Quit();
        Debug.Log("Quitting");
    }
}
