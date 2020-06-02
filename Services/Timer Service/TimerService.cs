using System;
using System.Collections.Generic;
using System.Linq;
using CatAstropheGames;
using UnityEngine;

/// <summary>
/// This service will work both in front and background.
/// </summary>
public class TimerService : ITimerService
{
    private ITimerState currentState;

    private Dictionary<int, long> timestampsOfLastUpdates = new Dictionary<int, long>();
    private Dictionary<int, List<Action<int>>> listeners = new Dictionary<int, List<Action<int>>>();

    private Dictionary<long, List<Action>> onceListeners = new Dictionary<long, List<Action>>();

    public TimerService()
    {
        SwitchToForeground();
    }

    private long Now()
    {
        return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }

    public void AddListenerOnTimer(Action<int> listener, int seconds)
    {
        if (!listeners.ContainsKey(seconds))
        {
            listeners.Add(seconds, new List<Action<int>>());
            timestampsOfLastUpdates.Add(seconds, Now());
        }
        listeners[seconds].Add(listener);
    }

    public void RemoveListenerOnTimer(Action<int> listener)
    {
        int index1 = -1;
        int index2 = -1;

        foreach (KeyValuePair<int, List<Action<int>>> tmp in listeners)
        {
            for (int i = 0; i < tmp.Value.Count; i++)
            {
                if (tmp.Value[i] == listener)
                {
                    index1 = tmp.Key;
                    index2 = i;
                }
            }
        }

        if (index1 == -1 || index2 == -1)
        {
            Debug.LogWarning("Could not find listener to remove.");
        }
        else
        {
            listeners[index1].Remove(listener);
        }
    }

    public void SwitchToBackground()
    {
        currentState?.Dispose();
        currentState = new BackgroundTimerState();
        currentState.AddListener(OnTick);
    }

    public void SwitchToForeground()
    {
        currentState?.Dispose();
        currentState = new ForegroundTimerState();
        currentState.AddListener(OnTick);
    }

    private void OnTick(int deltaSeconds)
    {
        long now = Now();
        Dictionary<int, bool> updated = new Dictionary<int, bool>();
        foreach (KeyValuePair<int, long> lastUpdate in timestampsOfLastUpdates)
        {
            int delta = (int)(now - lastUpdate.Value);
            if (lastUpdate.Key <= delta)
            {
                foreach (Action<int> listener in listeners[lastUpdate.Key])
                {
                    listener.SafeInvoke(delta);
                }

                if (!updated.ContainsKey(lastUpdate.Key))
                {
                    updated.Add(lastUpdate.Key, true);
                }
            }
        }

        foreach (KeyValuePair<int, bool> kvp in updated)
        {
            if (kvp.Value)
            {
                timestampsOfLastUpdates[kvp.Key] = now;
            }
        }

        foreach(KeyValuePair<long, List<Action>> kvp in onceListeners)
        {
            if (kvp.Key <= now)
            {
                foreach(Action action in kvp.Value)
                {
                    action.SafeInvoke();
                }
            }
        }

        List<long> toRemove = onceListeners.Keys.Where(t => t <= now).ToList();
        foreach (long key in toRemove)
        {
            onceListeners.Remove(key);
        }

    }

    public void AddListenerOnce(Action listener, int delay)
    {
        long timeToFire = Now() + delay;
        if (!onceListeners.ContainsKey(timeToFire))
        {
            onceListeners.Add(timeToFire, new List<Action>());
        }

        onceListeners[timeToFire].Add(listener);
    }
}
