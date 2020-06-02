using System;
using KKUserActivityRecognition;

namespace CatAstropheGames
{
    public interface IActivityService
    {
        void AddListenerOnActivityChange(Action<UserActivityInfo> onActivityUpdate);
        void StopSendingActivities();
        void StartSendingActivities();
    }
}