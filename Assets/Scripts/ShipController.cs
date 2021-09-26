using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    [Header("Ship variables")]
    [SerializeField] int maxVidas;
    [SerializeField] float recuperacionVidasSegundos;
    [SerializeField] float velocity;
    [SerializeField] float tiempoCargarHabilidad;
    [SerializeField] float intervaloCargarHabilidad;
    [SerializeField] ParticleSystem part1;
    [SerializeField] ParticleSystem part2;
    [SerializeField] ParticleSystem part3;
    [SerializeField] ParticleSystem part4;
    [SerializeField] ParticleSystem part5;
    [SerializeField] bool dying;

    [Space]

    [Header("Bullet variables")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shotDelay;
    float shotCounter = 0;
    float gettingshotCounter;
    float actionCounter = 0;
    float specialHabilityCharger = 0;
    int vidas;

    GameObject gameController;

    void Start ()
    {

        gameController = GameObject.FindWithTag("GameController");
        vidas = maxVidas;
        dying = false;
        transform.position = new Vector3(0, -4, 0);
        part1.Stop();
        part2.Stop();

        gettingshotCounter = recuperacionVidasSegundos;
    }
	
    public void shootMobile()
    {
            if (specialHabilityCharger < tiempoCargarHabilidad)
            {
                part1.Play();
                Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                part1.Play();

                Instantiate(bulletPrefab, transform.position + new Vector3(-1.6f, 0.8f, 0), Quaternion.Euler(0, 0, 0));

                Instantiate(bulletPrefab, transform.position + new Vector3(-0.8f, 0.8f, 0), Quaternion.Euler(0, 0, 0));

                Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.Euler(0, 0, 0));

                Instantiate(bulletPrefab, transform.position + new Vector3(0.8f, 0.8f, 0), Quaternion.Euler(0, 0, 0));

                Instantiate(bulletPrefab, transform.position + new Vector3(1.6f, 0.8f, 0), Quaternion.Euler(0, 0, 0));


                specialHabilityCharger = 0;
            }
            shotCounter = 0.0f;
     
    }

	void Update ()
    {
        if (!dying)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(
            (Input.GetKey("left") ? 1 : 0) * -velocity + (Input.GetKey("right") ? 1 : 0) * velocity,
            (Input.GetKey("down") ? 1 : 0) * -velocity / 2 + (Input.GetKey("up") ? 1 : 0) * velocity / 2);

            if (Input.GetKey("space") && shotCounter > shotDelay)
            {
                if (specialHabilityCharger < tiempoCargarHabilidad)
                {
                    part1.Play();
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.Euler(0, 0, 0));
                }
                else
                {
                    part1.Play();

                    Instantiate(bulletPrefab, transform.position + new Vector3(-1.6f, 0.8f, 0), Quaternion.Euler(0, 0, 0));

                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.8f, 0.8f, 0), Quaternion.Euler(0, 0, 0));

                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.Euler(0, 0, 0));

                    Instantiate(bulletPrefab, transform.position + new Vector3(0.8f, 0.8f, 0), Quaternion.Euler(0, 0, 0));

                    Instantiate(bulletPrefab, transform.position + new Vector3(1.6f, 0.8f, 0), Quaternion.Euler(0, 0, 0));


                    specialHabilityCharger = 0;
                }
                shotCounter = 0.0f;
            }

            if (shotCounter < shotDelay)
                shotCounter += Time.deltaTime;
        }

        if(gettingshotCounter <= 0)
        {
            gettingshotCounter = recuperacionVidasSegundos;
            if(vidas < maxVidas)
            vidas++;
            gameController.GetComponent<GameController>().changeVidas(vidas);
        }
        else
        {
            gettingshotCounter -= Time.deltaTime;
        }


        if(actionCounter < intervaloCargarHabilidad)
        {
            actionCounter += Time.deltaTime;
        }
        else
        {
            if (specialHabilityCharger <= tiempoCargarHabilidad + 0.5f) {
                specialHabilityCharger += Time.deltaTime;
                gameController.GetComponent<GameController>().specialHability(specialHabilityCharger, tiempoCargarHabilidad);
            }
        }

    }

    public void Damaged()
    {
        

        gettingshotCounter = recuperacionVidasSegundos;
        actionCounter = 0;

        if (vidas <= 1)
        {
            vidas--;
            gameController.GetComponent<GameController>().changeVidas(vidas);
            dying = true;
            part1.Stop();
            part3.Stop();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            part2.Play();
            Destroy(gameObject, part2.main.duration);
            gameController.GetComponent<GameController>().changeToScoreScreen();
        }
        else
        {
            vidas--;
            gameController.GetComponent<GameController>().changeVidas(vidas);
            part4.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("Enemy"))
            Damaged();
    }

    public void increaseCharge(float charge)
    {
        specialHabilityCharger = Mathf.Min(specialHabilityCharger + charge, tiempoCargarHabilidad);
    }

    public void playCombPart()
    {
        part5.Clear();
        part5.Play();
    }

    public void setRecVidas(float seg)
    {
        recuperacionVidasSegundos = seg;
    }

    public float getRecVidas()
    {
        return recuperacionVidasSegundos;
    }

}
