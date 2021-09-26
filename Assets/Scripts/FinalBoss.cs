using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : EnemyBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] GameObject falsePlayer;

    [Header("Wave 0 Variables")]
    [SerializeField] float maxAngle;
    [SerializeField] float rotSpeed;
    [SerializeField] GameObject turnOff;

    [Header("Wave 1 Variables")]
    [SerializeField] GameObject bullet1;
    [SerializeField] float wave1Vel;
    [SerializeField] float timeBetweenBullets1;
    [SerializeField] int waveCount1;


    float bullet1Counter = 0;
    Vector3 bullet1Dir = new Vector3(1, 0, 0);
    int lifeMaxCOunter = 0;
    Quaternion noRot;
    Vector3 noPos;
    Quaternion rotLeft;
    Quaternion rotRight;
    int interchanger = 0;

    protected override void Start ()
    {
        base.Start();
        rb2d.velocity = new Vector2(velocityX, velocityY);
        lifeMaxCOunter = lifes;
        noRot = transform.rotation;
        noPos = transform.position;
        rotLeft = Quaternion.Euler(0, 0, maxAngle);
        rotRight = Quaternion.Euler(0, 0, -maxAngle);
    }

    protected override void Update()
    {
        base.Update();


        if(lifes > lifeMaxCOunter / 2)
        {
            if (transform.position.x < -3.5f)
            {
                rb2d.velocity = new Vector2(Mathf.Abs(velocityX), velocityY);
                
            }

            if (transform.position.x > 3.5f)
            {
                rb2d.velocity = new Vector2(-Mathf.Abs(velocityX), velocityY);
                
            }
            if (rb2d.velocity.x > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotRight, rotSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotLeft, rotSpeed);
            }
        }

        if (lifes <= lifeMaxCOunter / 2)
        {
            turnOff.SetActive(false);
            velocityX = 0.0f;
            rb2d.velocity = new Vector3(0,0,0);
            timeToAttack = 999;
            transform.rotation = Quaternion.Slerp(transform.rotation, noRot, 0.05f);
            transform.position = Vector3.Lerp(transform.position, noPos, 0.05f);
            //disapro 1
            if (bullet1Counter < timeBetweenBullets1)
            {
                bullet1Counter += Time.deltaTime;
            }
            else
            {
                bullet1Counter = 0;
                for (int i = 0; i < waveCount1 - interchanger; i++)
                {
                    bullet1Dir = Quaternion.Euler(0, 0, -180 / (waveCount1 + 1 - interchanger)) * bullet1Dir;
                    GameObject inst = Instantiate(bullet1, transform.position + (bullet1Dir * 1), Quaternion.Euler(bullet1Dir.x, bullet1Dir.y, bullet1Dir.z)) as GameObject;
                    inst.SendMessage("setVel", bullet1Dir * wave1Vel);

                    
                }
                if (interchanger == 0)
                    interchanger = 1;
                else
                    interchanger = 0;
                bullet1Dir = new Vector3(1, 0, 0);
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject.Instantiate(falsePlayer, player.transform.position, player.transform.rotation);
        for(int i= 0; i<6; i++)
        player.GetComponent<ShipController>().Damaged();
    }

}