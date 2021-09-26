using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour {

    [Header("Asteroid variables")]
    [SerializeField] int lifes;

    ParticleSystem part;
    Animator anim;
    GameObject gameController;

    void Start()
    {
        anim = GetComponent<Animator>();
        part = GetComponent<ParticleSystem>();
        part.Stop();
        gameController = GameObject.FindWithTag("GameController");
    }

    public void Damaged()
    {
        gameController.GetComponent<GameController>().bulletsHit++;
        lifes--;
        if (lifes <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            part.Play();
            Destroy(gameObject, part.main.duration);
        }
        else
            anim.Play("Asteroid" + lifes.ToString());

    }

    public void DestroyIt()
    {
        lifes = 1;
        Damaged();
    }

}
