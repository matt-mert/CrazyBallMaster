using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private List<UnityEvent> unityEvents;

    private void Awake()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDestroy()
    {
        gameEvent.UnregisterListener(this);
    }

    public void RaiseEvents()
    {
        foreach (UnityEvent unityEvent in unityEvents)
        {
            unityEvent.Invoke();
        }
    }
}
