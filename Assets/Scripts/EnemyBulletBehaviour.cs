using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossBulletBehaviour1 : MonoBehaviour
{

    void Start()
    {
        //GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public void setVel(Vector3 vel)
    {
        GetComponent<Rigidbody2D>().velocity = vel;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            collision.GetComponent<AsteroidBehaviour>().Damaged();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ship"))
        {
            collision.GetComponent<ShipController>().Damaged();
            Destroy(gameObject);
        }
    }

}