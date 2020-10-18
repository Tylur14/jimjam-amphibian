using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Raft : EntityMover
{
    private PlayerMotor _passenger; // ref to player motor, the only thing that should be allowed to be a passenger on raft
    private Player _playerRef; // ref to player motor, the only thing that should be allowed to be a passenger on raft
    private bool _hasPassenger; // should only be player


    protected override void Start()
    {
        base.Start();
        _passenger = FindObjectOfType<PlayerMotor>();
        _playerRef = FindObjectOfType<Player>();
    }
    protected override void MoveLoop()
    {
        var pos = transform.position;
        pos.x += _moveAmount;
        if (Vector2.Distance(pos,new Vector2(_moveTarget,pos.y)) <= Mathf.Abs(_moveAmount))
            if(!_pingPong)
                pos.x = _startPosition;
            else if (_pingPong)
                PingPong();
        
        StartCoroutine(DoLoop());
        
        IEnumerator DoLoop()
        {
            yield return new WaitForSeconds(moveTime);
            if(_hasPassenger)
                _passenger.ProcessMove(_moveAmount,0f);
            transform.position = pos;
            _canDoMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _hasPassenger = true;
            _playerRef.safeFromWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _hasPassenger = false;
            _playerRef.safeFromWater = false;
        }
    }
}
