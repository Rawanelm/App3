using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject car1;
    public GameObject car2;

    private CarController car1Script;
    private CarController car2Script;

    public GameObject car1SpawnPoint;
    public GameObject car2SpawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        car1Script = car1.GetComponent<CarController>();
        car2Script = car2.GetComponent<CarController>();

    }

    // Update is called once per frame
    void Update()
    {
        MonitorCar(car1);
        MonitorCar(car2);
    }

    void MonitorCar(GameObject car){
        if(car == car1){
            if(car1Script.HEALTH <= 0){
                Respawn(car1, car1Script, car1SpawnPoint);
            }
        }
        if(car == car2){
            if(car2Script.HEALTH <= 0){
                Respawn(car2, car2Script, car2SpawnPoint);
            }
        }

    }

    void Respawn(GameObject car, CarController carScript, GameObject spawnPoint){
        car.transform.position = spawnPoint.transform.position;
        carScript.HEALTH = 100;
    }
}
