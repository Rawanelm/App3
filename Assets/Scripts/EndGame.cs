using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public string mainMenu;
    [SerializeField] private TextMeshProUGUI resultsDisplay;
    string winner;
    string loser;

    void Start()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().PlayMusic();
        winner = PlayerPrefs.GetString("P1name");
        loser = PlayerPrefs.GetString("P2name");

        ShowResults();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

    void ShowResults()
    {
        resultsDisplay.SetText("Winner: " + winner + "\nLoser: " + loser);
    }

}
