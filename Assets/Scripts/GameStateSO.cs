using UnityEngine;
using Unity.Collections;

public enum GameState
{
    Gameplay,
    Dead,
    Menu,
    Pause,
    Dialogue,
    Transport,
}

public class GameStateSO : ScriptableObject
{
    [SerializeField] [ReadOnly] private GameState currentGameState = default;
    [SerializeField] [ReadOnly] private GameState previousGameState = default;
    
    public GameState CurrentGameState => currentGameState;

    public void UpdateGameState(GameState newGameState)
    {
        if (newGameState == CurrentGameState) return;
        previousGameState = currentGameState;
        currentGameState = newGameState;
    }

    public void RestoreGameState()
    {
        if (previousGameState == currentGameState) return;
        (previousGameState, currentGameState) = (currentGameState, previousGameState);
    }
}
