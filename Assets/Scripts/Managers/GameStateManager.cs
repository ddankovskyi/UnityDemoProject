using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
   

    public GameState CurrentState { get; private set; } = GameState.Play;

    public bool IsInState(GameState state) => CurrentState == state;

    public event Action<GameState> OnGameStateChanged;

    public void TrySetState(GameState newState)
    {
        if(newState == CurrentState) return;
        if (CurrentState == GameState.GameOver) return;
        CurrentState = newState;
        OnGameStateChanged?.Invoke(CurrentState);
    }
    
}

public enum GameState
{

    Play,
    Inventory,
    GameOver
}
