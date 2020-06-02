using UnityEngine;
using System;
using CatAstropheGames;
using System.Collections.Generic;

public class ForegroundTimerState : ITimerState
{
    private List<Action<int>> listeners = new List<Action<int>>();

    public ForegroundTimerState()
    {
        TimeNotifyingMonoBehaviour.Instance.AddCallbackOnTimePassed(OnTimer);
        Application.runInBackground = true;
    }

    public void Dispose()
    {
        TimeNotifyingMonoBehaviour.Instance.RemoveCallbackOnTimePassed(OnTimer);
    }

    private void OnTimer(int delta)
    {
        foreach (Action<int> listener in listeners)
        {
            listener.SafeInvoke(delta);
        }
    }

    public void AddListener(Action<int> onTick)
    {
        listeners.Add(onTick);
    }
}
