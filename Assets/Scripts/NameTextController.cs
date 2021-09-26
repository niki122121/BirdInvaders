using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameTextController : MonoBehaviour {
    
    [SerializeField] Text name;

    void Start()
    {
        name.text = "";
    }

    void Update()
    {
        if (name.text.Length < 8)
        {
            if (Input.GetKeyDown(KeyCode.A))
                name.text += "A";
            else if (Input.GetKeyDown(KeyCode.B))
                name.text += "B";
            else if (Input.GetKeyDown(KeyCode.C))
                name.text += "C";
            else if (Input.GetKeyDown(KeyCode.D))
                name.text += "D";
            else if (Input.GetKeyDown(KeyCode.E))
                name.text += "E";
            else if (Input.GetKeyDown(KeyCode.F))
                name.text += "F";
            else if (Input.GetKeyDown(KeyCode.G))
                name.text += "G";
            else if (Input.GetKeyDown(KeyCode.H))
                name.text += "H";
            else if (Input.GetKeyDown(KeyCode.I))
                name.text += "I";
            else if (Input.GetKeyDown(KeyCode.J))
                name.text += "J";
            else if (Input.GetKeyDown(KeyCode.K))
                name.text += "K";
            else if (Input.GetKeyDown(KeyCode.L))
                name.text += "L";
            else if (Input.GetKeyDown(KeyCode.M))
                name.text += "M";
            else if (Input.GetKeyDown(KeyCode.N))
                name.text += "N";
            else if (Input.GetKeyDown(KeyCode.O))
                name.text += "O";
            else if (Input.GetKeyDown(KeyCode.P))
                name.text += "P";
            else if (Input.GetKeyDown(KeyCode.Q))
                name.text += "Q";
            else if (Input.GetKeyDown(KeyCode.R))
                name.text += "R";
            else if (Input.GetKeyDown(KeyCode.S))
                name.text += "S";
            else if (Input.GetKeyDown(KeyCode.T))
                name.text += "T";
            else if (Input.GetKeyDown(KeyCode.U))
                name.text += "U";
            else if (Input.GetKeyDown(KeyCode.V))
                name.text += "V";
            else if (Input.GetKeyDown(KeyCode.W))
                name.text += "W";
            else if (Input.GetKeyDown(KeyCode.X))
                name.text += "X";
            else if (Input.GetKeyDown(KeyCode.Y))
                name.text += "Y";
            else if (Input.GetKeyDown(KeyCode.Z))
                name.text += "Z";
            else if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
                name.text += "0";
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                name.text += "1";
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                name.text += "2";
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                name.text += "3";
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                name.text += "4";
            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
                name.text += "5";
            else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
                name.text += "6";
            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
                name.text += "7";
            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
                name.text += "8";
            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
                name.text += "9";
            else if (Input.GetKeyDown(KeyCode.Space))
                name.text += " ";
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && name.text.Length >= 1)
            name.text = name.text.Substring(0, name.text.Length - 1);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetString("name", name.text);
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
