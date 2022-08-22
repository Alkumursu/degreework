using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    //Characters
    bool canSwitch;
    bool characterHasDied = false;

    //Scenes
    public float restartDelay = 1f;
    [SerializeField] Image fadeToBlack;
    [SerializeField] float sceneLoadDelay = 3f;

    //Pause screen
    bool paused = false;
    //public GameObject pauseScreen;

    //Ending screen
    //public static bool gameIsWon;
    public GameObject gameWonScreen;

    private void Awake()
    {
        //gameIsWon = false;
        Instance = this;
    }
    void Start()
    {
        // testing below, previously EmmaActive
        UpdateGameState(GameState.MadisonActive);
        fadeToBlack.color = Color.black;
        fadeToBlack.DOFade(0f, sceneLoadDelay);
    }

    /*private void Update()
    {
        if (gameIsWon)
        {
            Debug.Log("Manager is Winning");
            gameWonScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            gameWonScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    */

    public void FadeIn(float fadeTime)
    {
        fadeToBlack.DOFade(1f, fadeTime);
    }

    public void FadeOut(float fadeTime)
    {
        fadeToBlack.DOFade(0f, fadeTime);
    }


    public void SetCharacterSwitchability(bool pSwitchability)
    {
        canSwitch = pSwitchability;
    }

    public bool GetCharacterSwitchability()
    {
        return canSwitch;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State)
        {
            /*case GameState.MainMenu:
                HandleMainMenu();
                break;
            */
            case GameState.PauseMenu:
                HandlePauseMenu();
                break;
            case GameState.MadisonActive:
                HandleMadisonActive();
                break;
            case GameState.EmmaActive:
                HandleEmmaActive();
                break;
            case GameState.GameWon:
                HandleGameWon();
                break;
            case GameState.LoadCheckpoint:
                HandleLoadCheckpoint();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(State), State, null);
        }

        OnGameStateChanged?.Invoke(State);
    }

    /*private void HandleMainMenu()
    {

    }
    */

    public void HandlePauseMenu()
    {
        //Debug.Log("Pause menu activated");
        //pauseScreen.gameObject.SetActive(true);
        //Time.timeScale = 0f;
    }
    

    private void HandleMadisonActive()
    {

    }

    private void HandleEmmaActive()
    {

    }

    public void HandleGameWon()
    {
        Debug.Log("Manager is Winning");
        gameWonScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HandleContinue()
    {
        FindObjectOfType<ControllableCharacter>().TriggerPauseMenu();
        paused = false;
    }

    public void HandleLoadCheckpoint()
    {
        if (characterHasDied == false)
        {
            characterHasDied = true;
            Debug.Log("Reload scene");
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        FindObjectOfType<ControllableCharacter>().TriggerPauseMenu();
        paused = false;

        fadeToBlack.DOFade(1f, sceneLoadDelay);
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMainMenu()
    {
        FindObjectOfType<ControllableCharacter>().TriggerPauseMenu();
        paused = false;

        fadeToBlack.DOFade(1f, sceneLoadDelay);
        DOTween.KillAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

public enum GameState
{
    MainMenu,
    PauseMenu,
    MadisonActive,
    EmmaActive,
    GameWon,
    LoadCheckpoint
}
