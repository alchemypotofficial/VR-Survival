using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventRedirector : MonoBehaviour
{
    public UnityEvent event1;
    public UnityEvent event2;
    public UnityEvent event3;

    public void PerformEvent1()
    {
        event1.Invoke();
    }
    public void PerformEvent2()
    {
        event2.Invoke();
    }
    public void PerformEvent3()
    {
        event3.Invoke();
    }
}
