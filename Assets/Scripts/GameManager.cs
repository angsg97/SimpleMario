using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : Singleton<GameManager>
{
    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;
    public static event gameEvent OnEnemyDeath;
    public Text score;
    private int playerScore = 0;

    public void increaseScore()
    {
        playerScore += 1;
        score.text = "SCORE: " + playerScore.ToString();
        spawnEnemy();
    }

    public void damagePlayer()
    {
        OnPlayerDeath();
    }

    public void spawnEnemy()
    {
        OnEnemyDeath();
    }

    public void resetScore()
    {
        playerScore = 0;
        score.text = "SCORE: " + playerScore.ToString();
    }
}