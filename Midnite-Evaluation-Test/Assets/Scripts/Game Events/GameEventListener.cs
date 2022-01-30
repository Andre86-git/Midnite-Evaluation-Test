using UnityEngine;

public abstract class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;

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

    public virtual void OnEventRaise()
    {
    }

    public virtual void OnEventRaise(Vector2 data)
    {
    }

    public virtual void OnEventRaise(Vector3 data)
    {
    }
}
