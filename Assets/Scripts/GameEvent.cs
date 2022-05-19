using System.Collections.Generic;
using UnityEngine;

public class GameEvent : ScriptableObject
{
    private HashSet<GameEventListener> listeners = new();

    public void Invoke()
    {
        foreach (GameEventListener globalEventListener in listeners)
        {
            globalEventListener.RaiseEvents();
        }
    }

    public void RegisterListener(GameEventListener listener) => listeners.Add(listener);
    public void UnregisterListener(GameEventListener listener) => listeners.Remove(listener);
}
