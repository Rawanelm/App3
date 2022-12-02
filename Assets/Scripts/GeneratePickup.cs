using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePickup : MonoBehaviour
{

    //public GameObject shieldPickUp;
    // public GameObject throwPickUp;
    //public GameObject gunPickUp;

    private int totalPickUps;
    private int maxPickups = 30;
    public GameObject[] pickUps;
    public float pickupSpawn = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        if (totalPickUps < maxPickups)
        {
            Invoke("SpawnPickUp", 1f / pickupSpawn);
        }
    }

    public void SpawnPickUp()
    {
        int ndx = Random.Range(0, pickUps.Length); //generates random number to choose type of enemy 
        GameObject go = Instantiate<GameObject>(pickUps[ndx]); //instatiates random enemy

        // determines the bounds for spawning pickUp
        Vector3 pos = Vector3.zero;
        float xMin = -121;
        float xMax = 51;

        float zMin = -195;
        float zMax = -26;

        // generates random x and y position
        pos.x = Random.Range(xMin, xMax);
        pos.z = Random.Range(zMin, zMax);
        pos.y = 1;

        go.transform.position = pos; //spawns the enemy
        totalPickUps++;

        Invoke("SpawnPickUp", 1f / pickupSpawn); // invokes the enemy spawn method again
    }

}
