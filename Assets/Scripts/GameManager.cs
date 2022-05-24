using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    bool canSwitch;
    bool characterHasDied = false;

    public float restartDelay = 1f;

    private void Awake()
    {
        Instance = this;
    }

    /*
    void Start()
    {
        UpdateGameState(GameState.EmmaActive);
    }
    */

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
            case GameState.MainMenu:
                HandleMainMenu();
                break;
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

    private void HandleMainMenu()
    {

    }

    private void HandlePauseMenu()
    {

    }

    private void HandleMadisonActive()
    {

    }

    private void HandleEmmaActive()
    {

    }

    private void HandleGameWon()
    {

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

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
