using System;
using System.Collections.Generic;
using CatAstropheGames;
using KKUserActivityRecognition;
using UnityEngine;

namespace CatAstropheGames
{
    public class ActivityService : IActivityService
    {
        private List<Action<UserActivityInfo>> listeners = new List<Action<UserActivityInfo>>();

        public ActivityService()
        {
            GameObject.FindObjectOfType<UserActivityListener>()
                .onUserActivityRecognized
                .AddListener(OnActivityUpdate);
        }

        public void StopSendingActivities()
        {
            UserActivityRecognition.StopUpdates();
        }

        public void StartSendingActivities()
        {
            UserActivityRecognition.StartUpdates();
        }

        private void OnActivityUpdate(UserActivityInfo arg0)
        {
            foreach (Action<UserActivityInfo> listener in listeners)
            {
                listener.SafeInvoke(arg0);
            }
        }

        public void AddListenerOnActivityChange(Action<UserActivityInfo> onActivityUpdate)
        {
            listeners.Add(onActivityUpdate);
        }
    }
}