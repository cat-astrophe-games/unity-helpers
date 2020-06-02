using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatAstropheGames
{
    public class MockPedometerService : IPedometerService
    {
        private List<Action<int>> listeners = new List<Action<int>>();
        private bool active;
        private const int stepsPerSecond = 5;


        public MockPedometerService(ITimerService timerService)
        {
            timerService.AddListenerOnTimer(TimePassed, 2);
        }

        public void AddDebugSteps(int stepsNumber)
        {
            TimePassed(stepsNumber / stepsPerSecond);
        }

        private void TimePassed(int deltaSeconds)
        {
            if (active)
            {
                int stepsDelta = Mathf.RoundToInt(deltaSeconds * stepsPerSecond);

                foreach (Action<int> listener in listeners)
                {
                    listener.SafeInvoke(stepsDelta);
                }
            }
        }

        public void SetActive(bool active)
        {
            if (active == this.active)
            {
                Debug.LogWarning($"Don't make this service active {active} twice.");
            }
            else
            {
                this.active = active;
            }
        }

        public void AddListenerOnSteps(Action<int> onSteps)
        {
            listeners.Add(onSteps);
        }

    }
}