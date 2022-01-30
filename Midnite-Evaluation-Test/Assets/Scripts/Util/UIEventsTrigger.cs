using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIEventsTrigger : MonoBehaviour
{
    public GameEvent resetEvent;
    public GameEvent newLevelEvent;

    public void TriggerReset()
    {
        if (resetEvent != null)
        {
            resetEvent.Raise();
        }
    }

    public void TriggerNew()
    {
        if (newLevelEvent != null)
        {
            newLevelEvent.Raise();
        }
    }
}
