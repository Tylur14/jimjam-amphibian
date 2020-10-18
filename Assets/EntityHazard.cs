using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHazard : EntityMover
{
    private Player _playerRef;

    private void Awake()
    {
        _playerRef = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playerRef.Die();
    }
}
