using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerVector3 : GameEventListener
{
    public UnityEvent<Vector3> response;

    public override void OnEventRaise(Vector3 data)
    {
        response.Invoke(data);
    }
}