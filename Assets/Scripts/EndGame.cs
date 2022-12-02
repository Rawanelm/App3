using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public string mainMenu;
    [SerializeField] private TextMeshProUGUI resultsDisplay;
    string p1Name;
    string p2Name;
    float p1Score;
    float p2Score;

    void Start()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().PlayMusic();
        p1Name = PlayerPrefs.GetString("P1name");
        p2Name = PlayerPrefs.GetString("P2name");
        p1Score = PlayerPrefs.GetFloat("Car1Score");
        p2Score = PlayerPrefs.GetFloat("Car2Score");

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
        if (p1Score > p2Score)
        {
            resultsDisplay.SetText("Winner: " + p1Name + "\nLoser: " + p2Name);
        }
        if (p2Score > p1Score)
        {
            resultsDisplay.SetText("Winner: " + p2Name + "\nLoser: " + p1Name);
        }
        if (p1Score == p2Score)
        {
            resultsDisplay.SetText(p2Name + " and " + p1Name + " have tied!");
        }
    }

    
    //if(PlayerPrefs.GetFloat("Car1Score") > PlayerPrefs.GetFloat("Car2Score")){return 0;}


}
