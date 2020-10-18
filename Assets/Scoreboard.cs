using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Scoreboard : MonoBehaviour
{

    [SerializeField] private TextMeshPro scoreDisplay;
    [SerializeField] private int score;

    private GameTimer _gameTimer;

    private void Awake()
    {
        _gameTimer = FindObjectOfType<GameTimer>();
    }

    // When the game is over or the game game starts this needs to be ran
    public void ResetScoreboard()
    {
        score = 0;
        UpdateScoreboard();
    }
    
    // Accessed when player picks up a score object
    public void AddScore(int scoreToAdd)
    {
        // increase current score then update scoreboard
        score += (int)Mathf.Abs(scoreToAdd  + _gameTimer.timer);
        UpdateScoreboard();
    }

    void UpdateScoreboard()
    {
        scoreDisplay.text = score.ToString();
    }
}
