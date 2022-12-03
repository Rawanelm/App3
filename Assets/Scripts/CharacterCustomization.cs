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
    public bool fieldCheck = false;
    public Button continueButton;
    public string gameScene;
    public string P1name = "";
    public string P2name = "";

    public GameObject d1;
    public GameObject d2;
   

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

        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioController>().StopMusic();
        SceneManager.LoadScene(gameScene);
    }
}