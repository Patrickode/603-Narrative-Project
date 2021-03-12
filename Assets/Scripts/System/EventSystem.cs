using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action OnEventA;
    public void EventA()
    {
        if(OnEventA != null)
        {
            OnEventA();
        }
    }

    public event Action OnEventB;
    public void EventB()
    {
        if (OnEventB != null)
        {
            OnEventB();
        }
    }

}
