using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    
    [Header("Enemy bullet variables")]
    [SerializeField] float velocity;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, -velocity, 0);
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