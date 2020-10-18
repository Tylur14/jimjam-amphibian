using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private Vector4 worldBounds;
    [SerializeField] private float moveAmount;
    public bool canMove = false;
    private AudioSource _aSrc;
    [SerializeField] private AudioClip moveSfx;

    private void Awake()
    {
        _aSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float x = 0;
        float y = 0;
        
        if (Input.GetKeyDown(KeyCode.A))
            x -= moveAmount;
        else if (Input.GetKeyDown(KeyCode.D))
            x += moveAmount;
        
        if (Input.GetKeyDown(KeyCode.S))
            y -= moveAmount;
        else if (Input.GetKeyDown(KeyCode.W))
            y += moveAmount;
        if(canMove && (x != 0 || y != 0))
            ProcessMove(x,y);
    }

    public void ProcessMove(float xMove, float yMove)
    {
        var pos = transform.position;
        pos.x = pos.x + xMove > worldBounds.x || pos.x + xMove < worldBounds.w ? pos.x = pos.x : pos.x += xMove;
        pos.y = pos.y + yMove > worldBounds.y || pos.y + yMove < worldBounds.z ? pos.y = pos.y : pos.y += yMove;
        _aSrc.PlayOneShot(moveSfx);
        transform.position = pos;
    }
}
