using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [Header("Game Controller variables")]
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] GameObject enemyBossPrefab;
    [SerializeField] GameObject bonusPrefab;
    [SerializeField] int score;

    [SerializeField] Text text;
    [SerializeField] Text textVidas;
    [SerializeField] Text textHab;
    [SerializeField] Text textCombo;
    [SerializeField] Text textMoneda;

    [Header("Standart Enemies")]
    [SerializeField] float timeBetweenEnemySpawn;
    [SerializeField] float minEnemySpawnTime;
    [SerializeField] float timeCounterEnemySpawn;
    [SerializeField] float enemySpawnVelocity;

    [Header("Boss Enemies")]
    [SerializeField] int flipX;

    [Header("Bonus Enemies")]
    [SerializeField] float timeBetweenBonusSpawn;
    [SerializeField] float timeCounterBonusSpawn;
    [SerializeField] float timeOffsetBonusSpawn;


    //para cambiar de nivel
    [SerializeField] int levelChangeScore;

    public int bulletsHit;

    public delegate void _ChangeColor(int color);
    public static event _ChangeColor ChangeColor;
    public delegate void _ChangeRandomColor();
    public static event _ChangeRandomColor ChangeRandomColor;

    //contador de menor 13 años
    public static bool easyModeOn;
    [SerializeField] int segundosMenor13;

    //contador tiempo de combo, se resetea después de 2 segundos
    int combo = 0;
    float comboCounter = 0;
    int monedasVirtuales;

    //dificultades
    public static float dificulty = 1.0f;

    void Start ()
    {
        /*PlayerPrefs.SetInt("score1", 0);
        PlayerPrefs.SetInt("score2", 0);
        PlayerPrefs.SetInt("score3", 0);
        PlayerPrefs.SetInt("score4", 0);
        PlayerPrefs.SetInt("score5", 0);
        PlayerPrefs.SetInt("score6", 0);
        PlayerPrefs.SetInt("score7", 0);
        PlayerPrefs.SetInt("score8", 0);
        PlayerPrefs.SetInt("score9", 0);
        PlayerPrefs.SetInt("score10", 0);
        PlayerPrefs.SetString("name1", "");
        PlayerPrefs.SetString("name2", "");
        PlayerPrefs.SetString("name3", "");
        PlayerPrefs.SetString("name4", "");
        PlayerPrefs.SetString("name5", "");
        PlayerPrefs.SetString("name6", "");
        PlayerPrefs.SetString("name7", "");
        PlayerPrefs.SetString("name8", "");
        PlayerPrefs.SetString("name9", "");
        PlayerPrefs.SetString("name10", "");*/
        bulletsHit = 0;
        timeCounterEnemySpawn = 0;
        timeCounterBonusSpawn = 0;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            score = 0;
            monedasVirtuales = 0;
        }


        PlayerPrefs.SetInt("score", score);

        PlayerPrefs.SetInt("monedasVirtuales", monedasVirtuales);
        textMoneda.text = "x" + monedasVirtuales.ToString();

        StartCoroutine("StartInstantiateBoss");

        textVidas.text = "x5";
        textHab.text = "0/10";
        textCombo.text = "0";

        if (flipX != 1 && flipX != -1)
        {
            flipX = 1;
            Debug.Log("asdasd");
        }

        timeBetweenEnemySpawn = timeBetweenEnemySpawn / dificulty;
        minEnemySpawnTime = minEnemySpawnTime / dificulty;

        //levelChangeScore = (int)((float)levelChangeScore * dificulty);
        //Debug.Log(levelChangeScore);
        GameObject.Find("Ship").GetComponent<ShipController>().setRecVidas(GameObject.Find("Ship").GetComponent<ShipController>().getRecVidas() * dificulty);

    }

    void Update()
    {
        monedasVirtuales = (int)(score / 376);
        if (enemyPrefab.Length > 0)
        {
            if (timeCounterEnemySpawn < timeBetweenEnemySpawn)
            {
                timeCounterEnemySpawn += Time.deltaTime;
            }
            else
            {
                if (timeBetweenEnemySpawn < minEnemySpawnTime)
                    timeBetweenEnemySpawn = minEnemySpawnTime;
                else if (timeBetweenEnemySpawn > minEnemySpawnTime)
                    timeBetweenEnemySpawn -= enemySpawnVelocity;

                int randEnemy = Random.Range(0, enemyPrefab.Length);
                Instantiate(enemyPrefab[randEnemy], new Vector3(Random.Range(-6.5f, 6.5f), 6.2f, 0), Quaternion.Euler(0, 0, 0));
                timeCounterEnemySpawn = 0;
            }
        }

        //crear avestruz
        if (bonusPrefab != null)
        {
            if (timeCounterBonusSpawn < timeBetweenBonusSpawn)
            {
                timeCounterBonusSpawn += Time.deltaTime;
            }
            else
            {
                //la posicion se ajusta del Start de la avestruz
                GameObject asd = Instantiate(bonusPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
                asd.GetComponent<AvestruzEnemyBehaviour>().adjustPos();
                timeCounterBonusSpawn = Random.Range(-timeOffsetBonusSpawn / 2, timeOffsetBonusSpawn / 2);
            }
        }


        //Comprobador de niveles
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 7)
        {
            easyModeDesactivated();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            easyModeActivated();
        }

        //jugar tiempo determinado
        if (easyModeOn && Time.time > segundosMenor13)
        {
            GameObject.Find("Ship").GetComponent<ShipController>().Damaged();
        }

        //cambiar nivel al tener puntuacion
        if (score >= levelChangeScore)
        {
            if (!easyModeOn)
            {
                if(SceneManager.GetActiveScene().buildIndex == 1)
                    GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(5);

                else if(SceneManager.GetActiveScene().buildIndex == 5)
                    GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(7);
            }
            else
            {
                if (SceneManager.GetActiveScene().buildIndex == 2)
                    GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(6);

                else if (SceneManager.GetActiveScene().buildIndex == 6)
                    GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(8);
            }
        }

        //combos
        if(comboCounter < 4.0f)
        {
            comboCounter += Time.deltaTime;
        }
        else
        {
            comboCounter = 0.0f;
            combo = 0;
        }
        textCombo.text = combo.ToString();

    }

    void LateUpdate()
    {
        if (bulletsHit == 1 && ChangeColor != null)
        {
            ChangeColor(Random.Range(0, 3));
        }
        else if (bulletsHit >= 2 && ChangeRandomColor != null)
        {
            ChangeRandomColor();
        }
        bulletsHit = 0;
    }

    IEnumerator StartInstantiateBoss()
    {
        if (enemyBossPrefab != null)
        {
            yield return new WaitForSecondsRealtime(10);
            StartCoroutine("InstantiateBoss");
        }
    }

    IEnumerator InstantiateBoss()
    {
        if (enemyBossPrefab != null)
        {
            Instantiate(enemyBossPrefab, new Vector3(-10.3f * flipX, 3.7f, 0), Quaternion.Euler(0, 0, 0));
            yield return new WaitForSecondsRealtime(10);
            StartCoroutine("InstantiateBoss");
        }
    }

    public void increaseScore(int points)
    {
        score += points;
        text.text = score.ToString();
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("monedasVirtuales", monedasVirtuales);
        textMoneda.text = "x" + monedasVirtuales.ToString();


        //mejorar carga de hab especial
        GameObject ship = GameObject.Find("Ship");
        ship.GetComponent<ShipController>().increaseCharge(points/100);


        //combos
        if (comboCounter < 4.0f)
        {
            combo ++;
            if(combo > 0 && combo%3 == 0)
            {
                ship.GetComponent<ShipController>().playCombPart();
            }
        }
    }

    public void changeVidas(int vidas)
    {
        textVidas.text = "x" + vidas.ToString();
    }

    public void specialHability(float current, float max)
    {
        textHab.text = (int)current + "/" + (int)max;
    }

    public void changeToScoreScreen()
    {
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(3);
    }

    public void easyModeActivated()
    {
        easyModeOn = true;
    }

    public void easyModeDesactivated()
    {
        easyModeOn = false;
    }

    public static void setDificulty(float dif)
    {
        dificulty = dif;
    }
    /*
    public void changeToLevelTwo()
    {
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(4);
    }*/

}
