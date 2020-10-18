using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sRender;
    [SerializeField] private Sprite[] playerStateSprites;
    [SerializeField] private Vector3 startingPos;

    private AudioSource _aSrc;
    [SerializeField] private AudioClip dieSfx;
    
    public bool safeFromWater = false;
    
    private PlayerMotor _motor;
    private GameController _gc;
    [HideInInspector] public bool isDead;
    private void Awake()
    {
        sRender.enabled = false;
        
        _aSrc = GetComponent<AudioSource>();
        _motor = GetComponent<PlayerMotor>();
        _gc = FindObjectOfType<GameController>();
        
        SoftReset();
    }
    public void Die()
    {
        // set canMove to false
        // set player sprite to 1
        if (!isDead)
        {
            _aSrc.PlayOneShot(dieSfx);
            _motor.canMove = false;
            sRender.sprite = playerStateSprites[1];
            _gc.EndRound();
            isDead = true;
        }
        
    }

    public void SoftReset()
    {
        _motor.canMove = false;
        transform.position = startingPos;
        sRender.enabled = false;
    }
    
    public void Reset()
    {
        isDead = false;
        transform.position = startingPos;
        sRender.enabled = true;
        sRender.sprite = playerStateSprites[0];
    }
}
