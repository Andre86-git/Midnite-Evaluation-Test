using System.Collections.Generic;
using UnityEngine;

public class GameEventWithData<T> : ScriptableObject
{
    protected List<GameEventWithDataListener<T>> eventListeners = new List<GameEventWithDataListener<T>>();

    public void RegisterListener(GameEventWithDataListener<T> listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventWithDataListener<T> listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }

    public void Raise(T data)
    {
        foreach (GameEventWithDataListener<T> listener in eventListeners)
        {
            listener.OnEventRaise(data);
        }
    }
}