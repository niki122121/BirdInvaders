using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToucanBulletBehaviour : MonoBehaviour
{
    
    [Header("Enemy bullet variables")]
    [SerializeField] float velocity;
    [SerializeField] float rotVel;
    [SerializeField] protected ParticleSystem part;

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
            playPart();
        }
        else if (collision.CompareTag("Ship"))
        {
            collision.GetComponent<ShipController>().Damaged();
            playPart();
        }
    }

    public void playPart()
    {
        part.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, part.main.duration);
    }

}