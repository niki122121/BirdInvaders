using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvestruzEnemyBehaviour : EnemyBehaviour {

    [Header("Enemy variables")]
    [SerializeField] SpriteRenderer spr;
    
    public int randDirPos;

    public override void adjustPos()
    {
        randDirPos = Random.Range(0, 2);
        rb2d.velocity = new Vector2(randDirPos == 0 ? velocityX : -velocityX, velocityY);
        transform.position = new Vector2(randDirPos == 0 ? -10 : 10, Random.Range(0, 4));
        spr.flipX = randDirPos == 0 ? true : false;
    }

    protected override void Update()
    {
        //base.update equivalente
        if (lifes > 0)
        {
            /*timeUntilAttack += Time.deltaTime;

            if (timeUntilAttack >= timeToAttack)
            {
                timeUntilAttack = 0;
                Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
                part.Clear();
                part.Play();
            }*/

            if (transform.position.y <= yGameOver)
                GameObject.FindWithTag("Ship").GetComponent<ShipController>().Damaged();

        }

        /*
        if (lifes > 0)
        {
            
            timeToChangeDirCounter += Time.deltaTime;

            if (timeToChangeDirCounter >= timeToChangeDir)
            {
                timeToChangeDirCounter = 0;
                rb2d.velocity = new Vector2(-rb2d.velocity.x, velocityY);
            }
            
        }*/
    }

    

}