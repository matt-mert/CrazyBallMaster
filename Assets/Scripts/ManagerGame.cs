using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame Instance { get; private set; }

    public enum GameStates
    {
        Menu,
        Playing,
        Dead,
        Reward,
        Completed,
    }

    public static GameStates CurrentState;

    public event Action OnMenuState;
    public event Action OnPlayingState;
    public event Action OnDeadState;
    public event Action OnRewardState;
    public event Action OnCompletedState;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
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
        if (OnMenuState == null) return;
        OnMenuState.Invoke();
        CurrentState = GameStates.Menu;
    }
    
    public void ChangeStateToPlaying()
    {
        if (OnPlayingState == null) return;
        OnPlayingState.Invoke();
        CurrentState = GameStates.Playing;
    }
    
    public void ChangeStateToDead()
    {
        if (OnDeadState == null) return;
        OnDeadState.Invoke();
        CurrentState = GameStates.Dead;
    }
    
    public void ChangeStateToReward()
    {
        if (OnRewardState == null) return;
        OnRewardState.Invoke();
        CurrentState = GameStates.Reward;
    }
    
    public void ChangeStateToCompleted()
    {
        if (OnCompletedState == null) return;
        OnCompletedState.Invoke();
        CurrentState = GameStates.Completed;
    }
}
