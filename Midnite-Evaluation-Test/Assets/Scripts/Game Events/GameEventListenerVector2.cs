using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerVector2 : GameEventListener
{
    public UnityEvent<Vector2> response;

    public override void OnEventRaise(Vector2 data)
    {
        response.Invoke(data);
    }
}