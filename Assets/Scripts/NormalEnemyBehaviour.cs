using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyBehaviour : EnemyBehaviour {

    [Header("Enemy variables")]
    [SerializeField] float timeToChangeDir;
    [SerializeField] Animator anim;
    int colorAnim;
    float timeToChangeDirCounter;

    void OnEnable()
    {
        GameController.ChangeColor += ChangeColor;
        GameController.ChangeRandomColor += ChangeRandomColor;
    }
    void OnDisable()
    {
        GameController.ChangeColor += ChangeColor;
        GameController.ChangeRandomColor += ChangeRandomColor;
    }

    void ChangeColor(int color)
    {
        if (lifes > 0)
        {
            colorAnim = color;
            ChangeParticlesColor(colorAnim);
            part.Clear();
            part.Play();
        }
    }

    void ChangeRandomColor()
    {
        if (lifes > 0)
        {
            colorAnim = Random.Range(0, 3);
            ChangeParticlesColor(colorAnim);
            part.Clear();
            part.Play();
        }
    }

    protected override void Start()
    {
        base.Start();
        ParticleSystem.MainModule ma = part.main;
        ma.startColor = new Color(0, 0.5647059f, 0.2980392f, 1);
        colorAnim = 0;
        anim.SetInteger("Color", 0);
        timeToChangeDirCounter = timeToChangeDir / 2;
        rb2d.velocity = new Vector2(Random.Range(0, 2) == 0 ? velocityX : -velocityX, velocityY);
    }

    protected override void Update()
    {
        base.Update();
        if (lifes > 0)
        {
            anim.SetInteger("Color", colorAnim);

            timeToChangeDirCounter += Time.deltaTime;

            if (timeToChangeDirCounter >= timeToChangeDir)
            {
                timeToChangeDirCounter = 0;
                rb2d.velocity = new Vector2(-rb2d.velocity.x, velocityY);
            }
            
        }
    }

    void ChangeParticlesColor (int color)
    {
        ParticleSystem.MainModule ma = part.main;
        
        if (color == 0)
            ma.startColor = new Color(0, 0.5647059f, 0.2980392f, 1);
        else if (color == 1)
            ma.startColor = new Color(0, 0.3764706f, 1, 1);
        else if (color == 2)
            ma.startColor = new Color(1, 0.2901961f, 0.3058824f, 1);
    }

}