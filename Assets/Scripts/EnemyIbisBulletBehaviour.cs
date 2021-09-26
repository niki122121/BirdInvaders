using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIbisBulletBehaviour : MonoBehaviour
{
    
    [Header("Enemy bullet variables")]
    [SerializeField] float velocity;
    [SerializeField] float rotVel;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, -velocity, 0);
    }

    void Update()
    {
        transform.Rotate(0, 0, rotVel);
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