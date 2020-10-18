using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EndZoneController : MonoBehaviour
{
    [SerializeField] private EndZone[] endZones;
    [SerializeField] private int activatedCount;
    private GameController _gc;

    private void Awake()
    {
        _gc = FindObjectOfType<GameController>();
    }

    public void RegisterActivation()
    {
        activatedCount++;
        if (activatedCount >= endZones.Length)
        {
            ResetZones();
            _gc.EndLevel();
        }
        else
        {
            _gc.SafelyEndRound();
        }
    }

    public void ResetZones()
    {
        foreach(var z in endZones)
            z.ResetZone();
        activatedCount = 0;
    }
}
