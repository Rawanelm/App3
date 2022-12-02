using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 100;
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
}
