using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreController : MonoBehaviour {

    [SerializeField] Text yourScore;
    [SerializeField] Text topScores;
    [SerializeField] Text topNames;
    
    void Start ()
    {
        yourScore.text = PlayerPrefs.GetString("name") + ": " + PlayerPrefs.GetInt("score");
        topScores.text = "";
        topNames.text = "";
        
        yourScore.text = PlayerPrefs.GetString("name") + ": " + PlayerPrefs.GetInt("score");

        int[] scores = new int[10];
        string[] names = new string[10];
        for (int j = 0; j < 10; j++)
        {
            scores[j] = PlayerPrefs.GetInt("score" + (j + 1), 0);
            names[j] = PlayerPrefs.GetString("name" + (j + 1), "VACÍO");
        }

        int i = 0;
        bool found = false;
        while (i < 10 && !found)
        {
            if (PlayerPrefs.GetInt("score") <= scores[i])
                i++;
            else
                found = true;
        }

        if (found)
        {
            for (int j = 9; j > i; j--)
            {
                scores[j] = scores[j - 1];
                names[j] = names[j - 1];
                PlayerPrefs.SetInt("score" + (j + 1), scores[j]);
                PlayerPrefs.SetString("name" + (j + 1), names[j]);
            }
            scores[i] = PlayerPrefs.GetInt("score");
            names[i] = PlayerPrefs.GetString("name");
            PlayerPrefs.SetInt("score" + (i + 1), PlayerPrefs.GetInt("score"));
            PlayerPrefs.SetString("name" + (i + 1), PlayerPrefs.GetString("name"));
        }

        for (int j = 0; j < 10; j++)
        {
            topNames.text += (j + 1) + ". " + names[j] + "\n";
            topScores.text += scores[j] + "\n";
        }
        
    }
	
}
