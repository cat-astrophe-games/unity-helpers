using BeliefEngine.HealthKit;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatAstropheGames
{
    public class PedometerOnHealthStoreService : IPedometerService
    {
        private List<Action<int>> listeners = new List<Action<int>>();

        private DateTimeOffset activeStart;
        private IPedometerWrapper pedometer;
        private int sumOfStepsSinceActivityStart = 0;
        private bool active;

        public PedometerOnHealthStoreService(IPedometerWrapper pedometer)
        {
            this.pedometer = pedometer;
        }

        public void AddDebugSteps(int stepsNumber)
        {
            sumOfStepsSinceActivityStart -= stepsNumber;
        }

        public void SetActive(bool active)
        {
            if (active == this.active)
            {
                Debug.LogWarning($"Don't set active with the same value twice {active}");
            } else
            {
                if (active)
                {
                    activeStart = DateTimeOffset.UtcNow;
                    sumOfStepsSinceActivityStart = 0;
                    this.pedometer.BeginReadingData(activeStart, delegate (List<PedometerData> data)
                    {
                        int steps = 0;
                        foreach (PedometerData sample in data)
                        {
                            steps += sample.numberOfSteps;
                        }
                        int stepsDelta = steps - sumOfStepsSinceActivityStart;
                        Debug.Log($"{steps} steps since {activeStart.ToString()}, steps delta {stepsDelta}");
                        OnPedometerStep(stepsDelta);
                        sumOfStepsSinceActivityStart = steps;
                    });
                }
                else
                {
                    this.pedometer.StopReadingData();
                }
            }
        }

        public void AddListenerOnSteps(Action<int> onSteps)
        {
            listeners.Add(onSteps);
        }

        private void OnPedometerStep(int steps)
        {
            foreach (Action<int> listener in listeners)
            {
                listener.SafeInvoke(steps);
            }
        }
    }
}