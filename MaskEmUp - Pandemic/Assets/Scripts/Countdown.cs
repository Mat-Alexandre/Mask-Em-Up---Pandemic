using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float timePerEnemy = 2.5f;
    public TMP_Text countText;
    public TMP_Text pplCountText;
    public GameObject gameMananger;

    private GameObject enemyManager;
    private float timeRemaining;
    private bool timerIsRunning = true;
    private int pplToSave;

    void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
        foreach(Transform child in enemyManager.transform)
        {
            timeRemaining++;
        }
        pplToSave = (int)timeRemaining;
        timeRemaining *= timePerEnemy;

        countText.text = timeRemaining.ToString();
        pplCountText.text = pplCountText.ToString();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Counting remaining time
                timeRemaining -= Time.deltaTime;
                countText.text = ((int)timeRemaining).ToString();

                // Counting saved peoples
                pplToSave = 0;
                foreach(Transform child in enemyManager.transform)
                {
                    if( !child.GetComponent<Enemy>().GetIsProtected() )
                    {
                        pplToSave++;
                    }
                }
                pplCountText.text = pplToSave.ToString();

                if(pplToSave == 0)
                {
                    gameMananger.GetComponent<GameMananger>().Victory();
                }
            }
            else
            {
                gameMananger.GetComponent<GameMananger>().GameOverTime();
                timeRemaining = 0;
                timerIsRunning = false;
            }

            
        }
    }


}
