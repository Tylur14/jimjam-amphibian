using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMover : MonoBehaviour
{
    protected enum MoveOptions
    {
        MoveLeft,
        MoveRight,
        PingPong,
        PingPongReverse
    };

    [Header("Movement Settings")] 
    [SerializeField] protected MoveOptions moveStyle;
    [SerializeField] protected float endPosition;
    [SerializeField] protected float moveTime;
    
    protected bool _pingPong;
    protected bool _canDoMove = true;
    
    protected float _moveTarget; // current destination
    protected float _startPosition; // where the entity is at start
    protected float _moveAmount = 0.25f; // how far to move, currently each tile is 0.25 in width & height
    
    protected virtual void Start()
    {
        SetupMover();
        MoveLoop();
    }
    
    protected virtual void MoveLoop()
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
            transform.position = pos;
            _canDoMove = true;
        }
    }
    
    protected void SetupMover()
    {
        _startPosition = transform.position.x;
        _moveAmount = Mathf.Abs(_moveAmount);
        _moveTarget = endPosition;
        switch (moveStyle)
        {
            case MoveOptions.MoveLeft: // move left to right
                _moveAmount = Mathf.Abs(_moveAmount)*-1;
                _pingPong = false;
                break;
            case MoveOptions.MoveRight: // move right to left
                _pingPong = false;
                break;
            case MoveOptions.PingPong: // move right then when reaching end point, invert moveAmount and go left
                _pingPong = true;
                break;
            case MoveOptions.PingPongReverse: // move left then when reaching end point, invert moveAmount and go right
                _moveAmount = Mathf.Abs(_moveAmount)*-1;
                _pingPong = true;
                break;
        }
    }
    
    protected void PingPong()
    {
        if (_moveTarget == _startPosition)
        {
            _moveTarget = endPosition;
            _moveAmount = Mathf.Abs(_moveAmount);
        }
        else if (_moveTarget == endPosition)
        {
            _moveTarget = _startPosition;
            _moveAmount = Mathf.Abs(_moveAmount)*-1;
        }
    }
    
    protected void Update()
    {
        if (_canDoMove)
        {
            _canDoMove = false;
            MoveLoop();
        }
    }
}
