using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class MyEvent : UnityEvent{}

public class TriggerEventsWrapper : MonoBehaviour
{
    public List<string> colliderTags = new List<string>();
    public MyEvent OnTriggerTouch;
    public MyEvent OnTriggerLeave;
    
    private void OnTriggerEnter(Collider other)
    {
        foreach (var tag in colliderTags)
        {
            if (other.tag == tag)
            {
                OnTriggerTouch.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach(var tag in colliderTags)
        {
            if (other.tag == tag)
            {
                OnTriggerLeave.Invoke();
            }
        }
    }
}
