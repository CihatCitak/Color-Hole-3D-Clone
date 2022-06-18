using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action OnGameLose;
    public static event Action OnGameWin;
    public static event Action OnObjectSwallowed;

    public static void InvokeOnGameLose()
    {
        OnGameLose?.Invoke();
    }

    public static void InvokeOnGameWin()
    {
        OnGameWin?.Invoke();
    }

    public static void InvokeOnObjectSwallowed()
    {
        OnObjectSwallowed?.Invoke();
    }
}
