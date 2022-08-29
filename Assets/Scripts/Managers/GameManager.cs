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

    //Ending screen
    public static bool gameIsWon;
    public GameObject gameWonScreen;

    private void Awake()
    {
        //gameIsWon = false;
        Instance = this;

        //testi
        //FindObjectOfType<AudioManager>().StopPlaying("MenuMusic");
        //FindObjectOfType<AudioManager>().Play("BGM");
    }
    void Start()
    {
        UpdateGameState(GameState.MadisonActive);
        fadeToBlack.color = Color.black;
        fadeToBlack.DOFade(0f, sceneLoadDelay);

        gameIsWon = false;
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
        gameIsWon = !gameIsWon;

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

    public void HandleContinue()
    {
        if (characterHasDied == false)
        {
            FindObjectOfType<ControllableCharacter>().TriggerPauseMenu();
        }
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
        if (characterHasDied == false)
        {
            FindObjectOfType<ControllableCharacter>().TriggerPauseMenu();
        }

        fadeToBlack.DOFade(1f, sceneLoadDelay);
        DOTween.KillAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMainMenu()
    {
        if (characterHasDied == false)
        {
            FindObjectOfType<ControllableCharacter>().TriggerPauseMenu();
        }

        fadeToBlack.DOFade(1f, sceneLoadDelay);
        DOTween.KillAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        //FindObjectOfType<AudioManager>().StopPlaying("BGM");
        //FindObjectOfType<AudioManager>().Play("MenuMusic");
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
