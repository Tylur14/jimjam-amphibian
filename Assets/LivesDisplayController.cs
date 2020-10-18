using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplayController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] lifeIcons;
    [SerializeField] private int currentLifeIndex;

    private void Awake()
    {
        ResetLifeCount();
    }

    private void Update()
    {
        //DEBUG remove life
        if(Input.GetKeyDown(KeyCode.K))
            RemoveLife();
    }

    public void ResetLifeCount()
    {
        currentLifeIndex = lifeIcons.Length - 1;
        foreach (var render in lifeIcons)
            render.enabled = true;
    }

    public bool RemoveLife()
    {
        if (currentLifeIndex < 0)
        {
            FindObjectOfType<GameController>().EndGame();
            return true;
        }
        
        if(currentLifeIndex >= 0)
            lifeIcons[currentLifeIndex].enabled = false;
        currentLifeIndex--;
        return false;
    }
}
