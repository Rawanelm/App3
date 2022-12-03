using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 50;
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
}
