using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombModelController : MonoBehaviour
{
    private string bombGameObjectName = "PickupItems/bomb";
    private GameObject bombObject;
    public GameObject bombPoint;


    public void Throw(){
        print("pew");
        bombObject = Instantiate(Resources.Load(bombGameObjectName, typeof(GameObject))) as GameObject;
        bombObject.transform.position = bombPoint.transform.position;
        bombObject.transform.rotation = bombPoint.transform.rotation;
    }
}
