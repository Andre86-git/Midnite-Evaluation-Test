using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Game Events/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    protected List<GameEventListener> eventListeners = new List<GameEventListener>();

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
}
