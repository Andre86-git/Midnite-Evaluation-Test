using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> eventListeners = new List<GameEventListener>();

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }

    public void Raise()
    {
        foreach (GameEventListener listener in eventListeners)
        {
            listener.OnEventRaise();
        }
    }

    public void Raise(Vector2 data)
    {
        foreach (GameEventListener listener in eventListeners)
        {
            listener.OnEventRaise(data);
        }
    }

    public void Raise(Vector3 data)
    {
        foreach (GameEventListener listener in eventListeners)
        {
            listener.OnEventRaise(data);
        }
    }
}
