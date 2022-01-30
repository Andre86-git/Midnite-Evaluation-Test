using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerVoid : GameEventListener
{
    public UnityEvent response;

    public override void OnEventRaise()
    {
        response.Invoke();
    }
}