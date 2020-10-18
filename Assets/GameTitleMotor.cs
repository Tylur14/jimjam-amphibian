using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitleMotor : MonoBehaviour
{
    [SerializeField] private float moveAmount = 0.25f;
    [SerializeField] private float moveTarget;
    [SerializeField] private float moveStart;
    [SerializeField] private float moveTime;

    private bool _canDoMove = true;
    
    private void Start()
    {
        MoveLoop();
    }

    private void Update()
    {
        if (_canDoMove)
        {
            _canDoMove = false;
            MoveLoop();
        }
    }

    void MoveLoop()
    {
        var pos = transform.position;
        pos.x += moveAmount;
        if (pos.x > moveTarget + moveAmount)
            pos.x = moveStart;
        StartCoroutine(DoLoop());
        IEnumerator DoLoop()
        {
            yield return new WaitForSeconds(moveTime);
            transform.position = pos;
            _canDoMove = true;
            //MoveLoop();
        }
    }

    private void OnDisable()
    {
        _canDoMove = true;
    }

    private void OnEnable()
    {
        var pos = transform.position;
        pos.x = moveStart;
        transform.position = pos;
    }
}
