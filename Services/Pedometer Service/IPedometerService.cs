using KKUserActivityRecognition;
using System;

namespace CatAstropheGames
{
    public interface IPedometerService
    {
        void SetActive(bool active);
        void AddListenerOnSteps(Action<int> onSteps);
        void AddDebugSteps(int stepsNumber);
    }
}