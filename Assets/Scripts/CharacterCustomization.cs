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
    public TMP_Dropdown p1Drop;
    public TMP_Dropdown p2Drop;
    private TMP_Text p1Selection;
    private TMP_Text p2Selection;
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

        p1Selection = p1Drop.captionText;
        P1hat = p1Selection.text;

        p2Selection = p2Drop.captionText;
        P2hat = p2Selection.text;

 
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
        else if (P1hat == "Crown")
        {
            PlayerPrefs.SetString("P1hat", "Hats/Crown");
        }
        else if (P1hat == "Magician Hat")
        {
            PlayerPrefs.SetString("P1hat", "Hats/MagicianHat");
        }
    }

    void PlayerTwoCustom()
    {
        if (P2hat == "Cowboy Hat")
        {
            PlayerPrefs.SetString("P2hat", "Hats/CowboyHat");
        }
        else if (P2hat == "Crown")
        {
            PlayerPrefs.SetString("P2hat", "Hats/Crown");
        }
        else if (P2hat == "Magician Hat")
        {
            PlayerPrefs.SetString("P2hat", "Hats/MagicianHat");
        }
    }
}