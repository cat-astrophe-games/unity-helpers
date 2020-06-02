using UnityEngine;
using System;
using System.Threading;
using CatAstropheGames;
using System.Collections.Generic;

public class BackgroundTimerState : ITimerState
{
    private readonly int frequencySeconds = 1;
    public Timer timer;
    private List<Action<int>> listeners = new List<Action<int>>();

    public BackgroundTimerState()
    {
        PlatformChecker.Platform platform = PlatformChecker.GetPlatform();
        switch (platform)
        {
            case PlatformChecker.Platform.Editor:
            default:
                TimeNotifyingMonoBehaviour.Instance.AddCallbackOnTimePassed(OnTimer);
                break;
            case PlatformChecker.Platform.iOS:
                timer?.Dispose();
                timer = new Timer(new TimerCallback(state => OnTimer(frequencySeconds)), null, 0, frequencySeconds * 1000);
                Background.StartTask();
                break;
        }

        Application.runInBackground = true;
    }

    public void Dispose()
    {
        PlatformChecker.Platform platform = PlatformChecker.GetPlatform();
        switch (platform)
        {
            case PlatformChecker.Platform.Editor:
            default:
                TimeNotifyingMonoBehaviour.Instance.RemoveCallbackOnTimePassed(OnTimer);
                break;
            case PlatformChecker.Platform.iOS:
                Background.StopTask();
                timer?.Dispose();
                break;
        }
    }

    private void OnTimer(int deltaSeconds)
    {
        foreach (Action<int> listener in listeners)
        {
            listener.SafeInvoke(deltaSeconds);
        }
    }

    public void AddListener(Action<int> onTick)
    {
        listeners.Add(onTick);
    }
}
