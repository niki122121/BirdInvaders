using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] Image tick;
    [SerializeField] Sprite yes;
    [SerializeField] Sprite no;

    [SerializeField] Image tick2;
    [SerializeField] Sprite yes2;
    [SerializeField] Sprite no2;

    [SerializeField] Image selectedDif;
    [SerializeField] Image dif1;
    [SerializeField] Image dif2;
    [SerializeField] Image dif3;

    [Header("dif variation")]
    [SerializeField] float dif1Var;
    [SerializeField] float dif2Var;
    [SerializeField] float dif3Var;


    public void BounceBullet ()
    {
        if (BulletBehaviour.bounce)
        {
            BulletBehaviour.bounce = false;
            tick.sprite = no;
        }
        else
        {
            BulletBehaviour.bounce = true;
            tick.sprite = yes;
        }
    }

    public void ColorBlind()
    {
        if (!MainCameraController.checkCB())
        {
            //activar daltonico
            tick2.sprite = yes2;
            MainCameraController.activateCB();
        }
        else
        {
            //desactivar daltonico
            tick2.sprite = no2;
            MainCameraController.activateNormal();
        }
    }

    public void dificultad(int n)
    {
        if(n == 1)
        {
            dif2.transform.position = selectedDif.transform.position;
            GameController.setDificulty(dif2Var);

            dif1.transform.position = new Vector2(-1000, 40);
            dif3.transform.position = new Vector2(-1000, 40);
        }
        if (n == 2)
        {
            dif3.transform.position = selectedDif.transform.position;
            GameController.setDificulty(dif3Var);

            dif1.transform.position = new Vector2(-1000, 40);
            dif2.transform.position = new Vector2(-1000, 40);
        }
        if (n == 3)
        {
            dif1.transform.position = selectedDif.transform.position;
            GameController.setDificulty(dif1Var);

            dif3.transform.position = new Vector2(-1000, 40);
            dif2.transform.position = new Vector2(-1000, 40);
        }
    }

}
