using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class ManagerGame : Singleton<ManagerGame>
{
    public enum GameStates
    {
        Menu,
        Playing,
        Dead,
        Reward,
        Completed,
    }

    public static GameStates CurrentState;

    
    public delegate void OnMenuState();
    public event OnMenuState onMenuState;
    public delegate void OnPlayingState();
    public event OnPlayingState onPlayingState;
    public delegate void OnDeadState();
    public event OnDeadState onDeadState;
    public delegate void OnRewardState();
    public event OnRewardState onRewardState;
    public delegate void OnCompletedState();
    public event OnCompletedState onCompletedState;
    
    public override void Awake()
    {
        base.Awake();
        CurrentState = GameStates.Playing;
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void OnSceneChange(Scene current, Scene next)
    {
        CurrentState = GameStates.Playing;
    }

    public void ChangeStateToMenu()
    {
        if (onMenuState == null) return;
        onMenuState.Invoke();
        CurrentState = GameStates.Menu;
    }
    
    public void ChangeStateToPlaying()
    {
        if (onPlayingState == null) return;
        onPlayingState.Invoke();
        CurrentState = GameStates.Playing;
    }
    
    public void ChangeStateToDead()
    {
        if (onDeadState == null) return;
        onDeadState.Invoke();
        CurrentState = GameStates.Dead;
    }
    
    public void ChangeStateToReward()
    {
        if (onRewardState == null) return;
        onRewardState.Invoke();
        CurrentState = GameStates.Reward;
    }
    
    public void ChangeStateToCompleted()
    {
        if (onCompletedState == null) return;
        onCompletedState.Invoke();
        CurrentState = GameStates.Completed;
    }
}
