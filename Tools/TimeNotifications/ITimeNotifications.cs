using System;

namespace CatAstropheGames
{
    public interface ITimeNotifications
    {
        void AddCallbackOnTimePassed(Action<int> listener);
    }
}