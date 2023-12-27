using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {none, menu, goalReached, playing, oops, gameOver}
public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager S;
    // Gamestate
    public GameState currentState = GameState.none;

    // UI elements
    public Text gameMessageText;
    public Text gameNameText;
    public Text scoreText;
    public Text timeText;

    // Game parameters
    public GameObject raccoonPrefab;
    private GameObject currentRaccoon;
    public GameObject lanesPrefab;
    private GameObject currentLanes;

    private int score = 0;
    private int livesRemaining;
    private float timeRemaining;
    private float TIME_AT_START = 30.0f;
    private int LIVES_AT_START = 3;

    //  // Will change this

    
    
    private void Awake(){
        if (S) {
            Destroy(this.gameObject);
        } else {

            // Singleton definition
            S = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Go to game menu
        SetGameMenu();
 
    }

    private void SetGameMenu(){

        // Set the state to menu
        currentState = GameState.menu;

        // Reset the game text
        gameMessageText.text = "Press \"Space\" to Start";
        gameNameText.text = "Trasher";
        gameMessageText.enabled = true;
        gameNameText.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        // Check for input event
        if(currentState == GameState.menu){

            //Wait for the prompt
            if(Input.GetKeyDown(KeyCode.Space)){

                //Launch new game
                InitializeGame();
            }
        }
    }

    private void InitializeGame(){

        // Reset the score & text value
        score = 0;
        scoreText.text = "000";

        // Reset the lives
        livesRemaining = LIVES_AT_START;

        // Reset the time
        timeRemaining = TIME_AT_START;

        // Reset the Lanes
        if(currentLanes){Destroy(currentLanes);}
        currentLanes = Instantiate(lanesPrefab);

        // Go to the get start state
        StartRound();

    }

    private void StartRound(){
        
        if(currentState == GameState.goalReached){
            if(currentLanes){Destroy(currentLanes);}
                currentLanes = Instantiate(lanesPrefab);
        }
        // Put us into playing start
        currentState = GameState.playing;
        
        // Update the UI
        gameMessageText.text = "Go!!!";
        gameMessageText.enabled = true;

        // Spawn the Raccoon
        if(currentRaccoon){Destroy(currentRaccoon);}
        currentRaccoon = Instantiate(raccoonPrefab);

        // Reset time
         timeRemaining = TIME_AT_START;

        // Turn off the message
        gameMessageText.enabled = false;
        gameNameText.enabled = false;

        // Start game countdown
        StartCoroutine(GameCountdown());

        // Play game background sound
        SoundManager.S.PlayGameSound();

    }

    public IEnumerator OopsState(){

        // Set the message
        gameMessageText.enabled = true;
        gameMessageText.text = "You have " + livesRemaining + " lives left!";

        yield return new WaitForSeconds(2.0f);

               // Decide whether the game is over or not
        if (livesRemaining <= 0){
            
            // the game is over
            GameOver();

        } else {
            // Reset the game
            StartRound();
        }

    }

    private void GameOver(){
        // Change the gamestate
        currentState = GameState.gameOver;

        // Display the game over message
        gameMessageText.enabled = true;
        gameMessageText.text = "Game Over!!!";
        
        StartCoroutine(PlayAgain());

    }

    public void RaccoonOutOfPlay(){

        // This is called when a raccoon gets hit by an obstacle
        
        // Stop sound
        SoundManager.S.StopSound();

        // Go to the oops state
        currentState = GameState.oops;

        livesRemaining--;

        StartCoroutine(OopsState());


    }

    public void GoalReached(){
        
        currentState = GameState.goalReached;

        SoundManager.S.StopSound();

        timeRemaining += 10.0f;

        //livesRemaining--;

        StartCoroutine(OopsState());
    }

    public void RaccoonScore(int Playerscore){
        // Update score
        score += Playerscore;
        scoreText.text = score.ToString();

    }


    IEnumerator GameCountdown(){
        while (timeRemaining > 0)
        {
            // Update your variable every second
            int secondsRemaining = Mathf.FloorToInt(timeRemaining);

            timeText.text = secondsRemaining.ToString();
            // Wait for one second
            yield return new WaitForSeconds(1.0f);

            // Decrease the countdown time by one second
            timeRemaining -= 1.0f;

            if (timeRemaining == 0.0f){
                RaccoonOutOfPlay();
            }
        }
    }

    public IEnumerator PlayAgain(){

        yield return new WaitForSeconds(2.0f);

        // Display score
        gameMessageText.text = "You Scored: " + score;

        yield return new WaitForSeconds(4.0f);
        gameMessageText.text = "Press \"Space\" to Play Again";

        // Change game state
        currentState = GameState.menu;

        yield return new WaitForSeconds(5.0f);
        gameMessageText.text = "Thanks for playing!";
        
        if(currentState == GameState.menu){
            SetGameMenu();
        }
        
    }
}

