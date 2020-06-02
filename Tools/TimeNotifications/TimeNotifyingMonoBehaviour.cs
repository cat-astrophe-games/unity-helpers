using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatAstropheGames
{
    public class TimeNotifyingMonoBehaviour : SingletonMonoBehaviour<TimeNotifyingMonoBehaviour>, ITimeNotifications
    {
        private float lastSeconds;
        private List<Action<int>> listeners = new List<Action<int>>();

        public void AddCallbackOnTimePassed(Action<int> listener)
        {
            listeners.Add(listener);
        }

        void Update()
        {
            if (Time.realtimeSinceStartup > lastSeconds)
            {
                int deltaTime = 1;
                foreach (Action<int> action in listeners)
                {
                    action.SafeInvoke(deltaTime);
                }

                lastSeconds = Time.realtimeSinceStartup + deltaTime;
            }
        }

        internal void RemoveCallbackOnTimePassed(Action<int> listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    }
}