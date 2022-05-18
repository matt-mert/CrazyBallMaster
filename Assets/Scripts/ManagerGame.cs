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
        
    }

    public void ChangeStateToMenu()
    {
        OnMenuState?.Invoke();
    }
    
    public void ChangeStateToPlaying()
    {
        OnPlayingState?.Invoke();
    }
    
    public void ChangeStateToDead()
    {
        OnDeadState?.Invoke();
    }
    
    public void ChangeStateToReward()
    {
        OnRewardState?.Invoke();
    }
    
    public void ChangeStateToCompleted()
    {
        OnCompletedState?.Invoke();
    }
}
