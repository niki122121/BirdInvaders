using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMenuBehaviour : MonoBehaviour {

    [Header("Ship Menu variables")]
    [SerializeField] float velocity;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(-velocity, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            transform.position = new Vector3(10.0f, -3.64f, 0);
        }
    }

}
