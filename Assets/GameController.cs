using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // need something for the level, score, timer, game state
    // ?each new level the timer is decreased by the level
    // |-> ?i.e. if the player is on level 20, the timer = baseTime - level;

    [Header("Display Objects")]
    [SerializeField] private GameObject titleObject;
    [SerializeField] private GameObject gameOverDisplayObject; // hide EXCEPT when game is over... duh?
    [SerializeField] private GameObject startGamePromptObject; // hide EXCEPT at game's main menu and initial wait delay is over
    [SerializeField] private LivesDisplayController livesDisplay;
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private GameTimer gameTimer;

    [Header("Player")]
    [SerializeField] private Player playerRef;
    [SerializeField] private PlayerMotor playerMotorRef;
    [SerializeField] private AudioClip winSfx;
    [SerializeField] private AudioClip loseSfx;
    [SerializeField] private AudioClip playStartSfx;
    [SerializeField] private AudioClip levelWinSfx;
    
    [Header("System Settings")]
    [SerializeField] private float initialWaitDelay; // allow time for the camera to zoom into the "computer screen" & prevent input until then
    [SerializeField] private float gameOverDelay;    // when the player has a game over, how long to wait before booting back to main menu
    [SerializeField] private float levelOverDelay;   // when the player reaches all end zones and completes a level
    [SerializeField] private float roundOverDelay;   // when the player dies but has lives left, how long until they are put back at the start and can continue


    private AudioSource _aSrc;
    private bool _canStartGame;
    private void Awake()
    {
        _aSrc = GetComponent<AudioSource>();
        
        // set score to zero or replace with feature to load in high score?
        scoreboard.ResetScoreboard();
        StartCoroutine(InitAfterDelay());
        IEnumerator InitAfterDelay()
        {
            yield return new WaitForSeconds(initialWaitDelay);
            startGamePromptObject.SetActive(true);
            _canStartGame = true;
            playerRef.Reset();
            playerMotorRef.canMove = false;
        }
    }

    private void Update()
    {
        if (_canStartGame && Input.GetKeyDown(KeyCode.Space))
        {
            _canStartGame = false;
            StartGame();
        }
    }

    void StartGame()
    {
        // set score to zero
        scoreboard.ResetScoreboard();
        
        // clear game field
        titleObject.SetActive(false);
        startGamePromptObject.SetActive(false);
        livesDisplay.ResetLifeCount();
        StartLevel();
    }

    void StartLevel()
    {
        
        // play starting jingle
        StartRound();
        
    }

    void StartRound()
    {
        
        StartCoroutine(NextRound());
        IEnumerator NextRound()
        {
            yield return new WaitForSeconds(roundOverDelay);
            _aSrc.PlayOneShot(playStartSfx);
            playerRef.Reset();
            playerMotorRef.canMove = true;
            gameTimer.StartTimer(40); // ? replace with level based timer?
        }
    }

    // bad ending
    public void EndRound()
    {
        
        gameTimer.StopClock(); 
        if (!livesDisplay.RemoveLife())
            StartRound();
    }

    // good ending
    public void SafelyEndRound()
    {
        _aSrc.PlayOneShot(winSfx);
        gameTimer.StopClock(); 
        playerRef.SoftReset();
        playerMotorRef.canMove = false;
        StartRound();
    }

    public void EndLevel()
    {
        // only reached if the player gets to all four houses in the level
        // player win jingle
        _aSrc.PlayOneShot(levelWinSfx);
        // disable & hide player
        playerRef.SoftReset();
        
        gameTimer.StopClock();
        // reset end zones
        StopAllCoroutines();
        StartCoroutine(NextLevel());
        IEnumerator NextLevel()
        {
            yield return new WaitForSeconds(levelOverDelay);
            StartLevel();
        }
    }

    public void EndGame()
    {
        _aSrc.PlayOneShot(loseSfx);
        FindObjectOfType<EndZoneController>().ResetZones();
        
        // only reached if the player loses all three lives
        
        // display game over text
        gameOverDisplayObject.SetActive(true);
        print("Gaming over");
        // start delay, the player cannot do anything in this state
        // |-> after delay send them back to the state the game is at when first booted
        StartCoroutine(GameOver());
        IEnumerator GameOver()
        {
            yield return new WaitForSeconds(gameOverDelay);
            
            gameOverDisplayObject.SetActive(false);
            
            titleObject.SetActive(true);
            
            startGamePromptObject.SetActive(true);
            
            _canStartGame = true;
        }
    }
}
