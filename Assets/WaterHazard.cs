using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHazard : MonoBehaviour
{
    [SerializeField] private float deathTimer = 0.15f;
    private Player _playerRef;
    private bool _canAttemptDrown;

    private void Awake()
    {
        _playerRef = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        if(_canAttemptDrown)
            if (!_playerRef.safeFromWater && !_playerRef.isDead)
            {
                deathTimer -= Time.deltaTime;
                if (deathTimer <= 0)
                {
                    _playerRef.Die();
                    _canAttemptDrown = false;
                    deathTimer = 0.15f;
                }
            }
            else deathTimer = 0.15f;
                
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _canAttemptDrown = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _canAttemptDrown = false;
    }
}
