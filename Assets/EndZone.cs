using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EndZone : MonoBehaviour
{
    private bool _isActivated;
    private EndZoneController _ezController;
    private GameObject _pushBackBlock;
    private SpriteRenderer _renderer;

    private Scoreboard _scoreboard;
    
    private void Awake()
    {
        _pushBackBlock = transform.GetChild(0).gameObject;
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
        
        _ezController = FindObjectOfType<EndZoneController>();
        _scoreboard = FindObjectOfType<Scoreboard>();
    }

    public void ResetZone()
    {
        _isActivated = false;
        _pushBackBlock.SetActive(false);
        _renderer.enabled = false;
    }
    
    void ActivateZone()
    {
        print("Activating tile");
        _isActivated = true;
        _pushBackBlock.SetActive(true);
        _renderer.enabled = true;
        _ezController.RegisterActivation();
        _scoreboard.AddScore(100);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_isActivated)
            if(other.CompareTag("Player"))
                ActivateZone();
    }
}
