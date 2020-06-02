using System;
using System.Collections.Generic;
using CatAstropheGames;
using KKUserActivityRecognition;
using Opencoding.CommandHandlerSystem;
using UnityEngine;

namespace CatAstropheGames
{
    public class MockActivityService : IActivityService
    {
        private List<Action<UserActivityInfo>> listeners = new List<Action<UserActivityInfo>>();
        private bool isActive;
        public ActivityType mockActivityType = ActivityType.running;

        public MockActivityService(ITimeNotifications timeNotifier)
        {
            timeNotifier.AddCallbackOnTimePassed(TimePassed);
            CommandHandlers.RegisterCommandHandlers(this);
        }

        ~MockActivityService()
        {
            CommandHandlers.UnregisterCommandHandlers(this);
        }

        private void TimePassed(int seconds)
        {
            if (isActive)
            {
                UserActivityInfo userActivityInfo = new UserActivityInfo(
                    new Dictionary<ActivityType, bool>()
                    {
                        {ActivityType.walking, ActivityType.walking == mockActivityType},
                        {ActivityType.automative, ActivityType.automative == mockActivityType},
                        {ActivityType.cycling, ActivityType.cycling == mockActivityType},
                        {ActivityType.running, ActivityType.running == mockActivityType},
                        {ActivityType.stationary, ActivityType.stationary == mockActivityType},
                        {ActivityType.unknown, ActivityType.unknown == mockActivityType}
                    },
                    ConfidenceLevel.high,
                    DateTime.Now.Subtract(new TimeSpan(0, 0, seconds))
                );

                foreach (Action<UserActivityInfo> listener in listeners)
                {
                    listener.SafeInvoke(userActivityInfo);
                }
            }
        }

        public void AddListenerOnActivityChange(Action<UserActivityInfo> onActivityUpdate)
        {
            listeners.Add(onActivityUpdate);
        }

        public void StopSendingActivities()
        {
            isActive = false;
        }

        public void StartSendingActivities()
        {
            isActive = true;
        }

        [CommandHandler(Description = "Change Activity type, see hints for hints")]
        public void ChangeMockActivityType(ActivityType newType)
        {
            Debug.Log("Changing mock activity type to: " + newType);
            mockActivityType = newType;
        }
    }
}