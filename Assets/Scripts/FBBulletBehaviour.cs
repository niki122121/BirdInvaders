using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBBulletBehaviour : MonoBehaviour {

    public static bool bounce = true;

    [Header("Bullet variables")]
    [SerializeField] float velocity;
    [SerializeField] bool firstBounce;
    Rigidbody2D rb2d;
	
	void Start ()
    {
        firstBounce = true;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(0, velocity, 0);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBehaviour>().Damaged();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ship"))
        {
            collision.GetComponent<ShipController>().Damaged();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("BounceBullet"))
        {
            if (firstBounce)
            {
                changeDirection(Random.Range(200.0f, 340.0f));
                firstBounce = false;
            }
            else
                changeDirection(Vector2.SignedAngle(new Vector2(rb2d.velocity.x, rb2d.velocity.y), Vector2.right));
        }
        else if (collision.CompareTag("Wall"))
            changeDirection(Vector2.SignedAngle(new Vector2(rb2d.velocity.x, rb2d.velocity.y), Vector2.right) + 180);
        else if (collision.CompareTag("Asteroid"))
        {
            collision.GetComponent<AsteroidBehaviour>().Damaged();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("ToucanBullet"))
        {
            collision.GetComponent<EnemyToucanBulletBehaviour>().playPart();
            Destroy(gameObject);
        }
    }

    void changeDirection(float angle)
    {
        if (bounce)
        {
            rb2d.velocity = new Vector2(velocity * Mathf.Cos(angle * Mathf.Deg2Rad), velocity * Mathf.Sin(angle * Mathf.Deg2Rad));
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

}