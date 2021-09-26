using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToucanBehaviour : EnemyBehaviour {

	protected override void Start ()
    {
        base.Start();
        rb2d.velocity = new Vector2(velocityX, velocityY);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}