using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private string bulletGameObjectName = "PickupItems/bullet";
    private GameObject bulletObject;
    public GameObject bulletPoint;


    public void ShootGun(){
        print("pew");
        bulletObject = Instantiate(Resources.Load(bulletGameObjectName, typeof(GameObject))) as GameObject;
        bulletObject.transform.position = bulletPoint.transform.position;
        bulletObject.transform.rotation = bulletPoint.transform.rotation;
    }
}
