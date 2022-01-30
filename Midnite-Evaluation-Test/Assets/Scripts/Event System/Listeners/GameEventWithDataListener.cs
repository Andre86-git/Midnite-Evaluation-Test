using UnityEngine;
using UnityEngine.Events;

public class GameEventWithDataListener<T> : MonoBehaviour
{
    public GameEventWithData<T> gameEvent;
    public UnityEvent<T> response;

    public void OnEnable()
    {
        if (gameEvent != null)
        {
            gameEvent.RegisterListener(this);
        }
    }

    public void OnDisable()
    {
        if (gameEvent != null)
        {
            gameEvent.UnregisterListener(this);
        }
    }

    public virtual void OnEventRaise(T data)
    {
        if (response != null)
        {
            response.Invoke(data);
        }
    }
}