using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMananger : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject winUI;

    private bool victory = false;
    private bool gameOver = false;

    public void GameOverTime()
    {
        if(!victory && !gameOver)
        {
            Debug.Log("Time's over");
            gameOverUI.SetActive(true);
            gameOver = true;
        }
    }

    public void GameOverVirus()
    {
        if(!victory && !gameOver)
        {
            Debug.Log("Everyone's infected");
            gameOverUI.SetActive(true);
            gameOver = true;

        }
    }

    public void Victory()
    {
        if(!victory && !gameOver)
        {
            Debug.Log("You Win");
            winUI.SetActive(true);
            victory = true;
        }
    }
}
