using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CharacterCustomization : MonoBehaviour
{
    public TMP_InputField playerOneName;
    public TMP_InputField playerTwoName;
    //public TMP_Text p1Selection;
    //public TMP_Text p2Selection;
    public bool fieldCheck = false;
    public Button continueButton;
    public string gameScene;
    public string P1name = "";
    public string P2name = "";
    public string P1hat = "";
    public string P2hat = "";

    void Update()
    {
        if (playerOneName.GetComponent<TMP_InputField>().text.Length > 0 && playerTwoName.GetComponent<TMP_InputField>().text.Length > 0)
        { 
            fieldCheck = true;
        }

        if (fieldCheck == true && continueButton.interactable == false)
        {
            continueButton.interactable = true;
        }
    }

    public void BeginGame()
    {
        P1name = playerOneName.GetComponent<TMP_InputField>().text;
        PlayerPrefs.SetString("P1name", P1name);
        P2name = playerTwoName.GetComponent<TMP_InputField>().text;
        PlayerPrefs.SetString("P2name", P2name);

        P1hat = "Cowboy Hat";
            //p1Selection.GetComponent<TMP_Text>().ToString();
        P2hat = "Crown";
        
        //p2Selection.GetComponent<TMP_Text>().ToString();

        Debug.Log(P1hat);
        Debug.Log(P2hat);
        PlayerOneCustom();
        PlayerTwoCustom();

        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().StopMusic();
        SceneManager.LoadScene(gameScene);
    }

    void PlayerOneCustom()
    {
        if(P1hat == "Cowboy Hat")
        {
            PlayerPrefs.SetString("P1hat", "Hats/CowboyHat");
        }
        if (P1hat == "Crown")
        {
            PlayerPrefs.SetString("P1hat", "Hats/Crown");
        }
        else
        {
            PlayerPrefs.SetString("P1hat", "Hats/MagicianHat");
        }
    }

    void PlayerTwoCustom()
    {
        if (P1hat == "Cowboy Hat")
        {
            PlayerPrefs.SetString("P2hat", "Hats/CowboyHat");
        }
        if (P1hat == "Crown")
        {
            PlayerPrefs.SetString("P2hat", "Hats/Crown");
        }
        else
        {
            PlayerPrefs.SetString("P2hat", "Hats/MagicianHat");
        }
    }
}