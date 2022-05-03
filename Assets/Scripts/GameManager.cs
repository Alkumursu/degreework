using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    bool canSwitch;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.EmmaActive); //for test purposes, change when game developed further
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
        /*if (State == newState)
        {
            return;
        }*/

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
        throw new NotImplementedException();
    }

    private void HandlePauseMenu()
    {
        throw new NotImplementedException();
    }

    private void HandleMadisonActive()
    {

    }

    private void HandleEmmaActive()
    {

    }

    private void HandleGameWon()
    {
        throw new NotImplementedException();
    }

    private void HandleLoadCheckpoint()
    {
        throw new NotImplementedException();
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
