using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushback : MonoBehaviour
{
    private PlayerMotor _pm;
    private void Start()
    {
        _pm = FindObjectOfType<PlayerMotor>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            _pm.ProcessMove(0f,-.25f);
    }
}
