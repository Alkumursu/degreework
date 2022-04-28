using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.EmmaActive); //for test purposes, change when game developed further
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.OnlyMadison:
                break;
            case GameState.OnlyEmma:
                break;
            case GameState.MadisonActive:
                break;
            case GameState.EmmaActive:
                break;
            case GameState.GameWon:
                break;
            case GameState.LoadCheckpoint:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    OnlyMadison, //At the start of the game when only madison is playable
    OnlyEmma,
    MadisonActive,
    EmmaActive,
    GameWon,
    LoadCheckpoint
}
