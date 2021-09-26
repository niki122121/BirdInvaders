using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    [Header("Enemy variables")]
    [SerializeField] protected int lifes;
    [SerializeField] protected float velocityY;
    [SerializeField] protected float velocityX;
    [SerializeField] protected int scoreWhenDying;
    [SerializeField] protected float yGameOver;
    [SerializeField] protected ParticleSystem part;
    [SerializeField] protected Rigidbody2D rb2d;

    [Space]

    [Header("Bullet Variables")]
    [SerializeField] protected GameObject enemyBulletPrefab;
    [SerializeField] protected float timeToAttack;
    [SerializeField] protected float timeUntilAttack;

	virtual protected void Start ()
    {
        timeUntilAttack = 0;
    }

    virtual protected void Update()
    {
        
        if (lifes > 0)
        {
            timeUntilAttack += Time.deltaTime;

            if (timeUntilAttack >= timeToAttack)
            {
                timeUntilAttack = 0;
                Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            }

            if (transform.position.y <= yGameOver)
                GameObject.FindWithTag("Ship").GetComponent<ShipController>().Damaged();

        }
    }

    public void Damaged ()
    {
        if (lifes > 0)
        {
            lifes--;
            part.Clear();
            part.Play();
            if (lifes <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                GameObject gameController = GameObject.FindWithTag("GameController");
                gameController.GetComponent<GameController>().increaseScore(scoreWhenDying);
                Destroy(gameObject, part.main.duration);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            collision.GetComponent<AsteroidBehaviour>().DestroyIt();
            lifes = 1;
            Damaged();
        }
    }

    virtual public void adjustPos()
    {
        Debug.Log("wrong method");
    }

}