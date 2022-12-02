using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;

    private CarController car1Script;
    private CarController car2Script;

    public GameObject car1SpawnPoint;
    public GameObject car2SpawnPoint;

    public string gameOverScene;

    public float timeLeft = 120;
    private int timeLeftInt;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        car1Script = car1.GetComponent<CarController>();
        car2Script = car2.GetComponent<CarController>();
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        MonitorCar(car1);
        MonitorCar(car2);

        timeLeft -= Time.deltaTime;
        timeLeftInt = (int)timeLeft;
        timerText.text = "timer: " + timeLeftInt.ToString();
        if ( timeLeft < 0 )
        {
            EndGame();
        }
    }

    void MonitorCar(GameObject car){
        if(car == car1){
            if(car1Script.health <= 0){
                Respawn(car1, car1Script, car1SpawnPoint);
                car2Script.AddPoint();
            }
        }
        if(car == car2){
            if(car2Script.health <= 0){
                Respawn(car2, car2Script, car2SpawnPoint);
                car1Script.AddPoint();
            }
        }

    }

    void Respawn(GameObject car, CarController carScript, GameObject spawnPoint){
        car.transform.position = spawnPoint.transform.position;
        carScript.health = 100;
    }


    public void PlayMusic()
    {
        GameObject.FindGameObjectWithTag("GameMusic").GetComponent<GameAudio>().PlayMusic();
    }

    public void EndGame()
    {
        GameObject.FindGameObjectWithTag("GameMusic").GetComponent<GameAudio>().StopMusic();
        SceneManager.LoadScene(gameOverScene);
    }
}
