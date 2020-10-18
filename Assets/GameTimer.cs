using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GameTimer : MonoBehaviour
{
    public float timer;
    [SerializeField] private TextMeshPro timerDisplay;
    private bool _active;
    private Player _playerRef;

    private void Awake()
    {
        _playerRef = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (_active)
        {
            timer -= Time.deltaTime;
            timerDisplay.text = "Time:" + timer.ToString("00");
            if (timer < 0)
            {
                timerDisplay.text = "Time:00";
                _playerRef.Die();
                _active = false;
            }
        }
    }

    public void StartTimer(int time)
    {
        timer = time;
        _active = true;
    }

    public void StopClock()
    {
        _active = false;
        timerDisplay.text = "Time:" + timer.ToString("00");
    }
}
