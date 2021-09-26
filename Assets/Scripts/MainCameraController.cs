using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] Camera normalCam;
    [SerializeField] Camera colorBlindCam;

    public static Camera normalCamera;
    public static Camera colorBlindCamera;
    static bool colorBlindEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        normalCamera = normalCam;
        colorBlindCamera = colorBlindCam;
        initialize();
    }
   

    public static bool checkCB()
    {
        Debug.Log(colorBlindEnabled);
        return colorBlindEnabled;
    }

    public static void setCB(bool asd)
    {
        colorBlindEnabled = asd;
    }

    public static void activateCB()
    {
        colorBlindEnabled = true;
        normalCamera.transform.gameObject.tag = "OffCamera";
        colorBlindCamera.transform.gameObject.tag = "MainCamera";
        colorBlindCamera.gameObject.SetActive(true);
        normalCamera.gameObject.SetActive(false);
    }

        public static void activateNormal()
    {
        colorBlindEnabled = false;
        normalCamera.transform.gameObject.tag = "MainCamera";
        colorBlindCamera.transform.gameObject.tag = "OffCamera";
        normalCamera.gameObject.SetActive(true);
        colorBlindCamera.gameObject.SetActive(false);
    }


    public static void initialize()
    {
        if (checkCB())
        {
            activateCB();
        }
        else
        {
            activateNormal();
        }
    }
}
