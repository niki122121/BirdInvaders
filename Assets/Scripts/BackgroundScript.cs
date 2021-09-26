using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] GameObject tile1;
    [SerializeField] GameObject tile2;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tile1.transform.position = new Vector2(0, tile1.transform.position.y - speed * Time.deltaTime);
        tile2.transform.position = new Vector2(0, tile2.transform.position.y - speed * Time.deltaTime);

        if(tile1.transform.position.y < -10)
        {
            tile1.transform.position = new Vector2(0, 10.5f);
        }
        if (tile2.transform.position.y < -10)
        {
            tile2.transform.position = new Vector2(0, 10.5f);
        }
    }
}
