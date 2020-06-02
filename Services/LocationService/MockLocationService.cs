using System;
using CatAstropheGames;
using Opencoding.CommandHandlerSystem;
using UnityEngine;

namespace CatAstropheGames
{
    public class MockLocationService : ILocationService
    {

        private bool hasInitialized = false;
        private GPSPos currentPos;
        private GPSPos realPos;
        private double currentAngleDegrees;
        private float radius = 0.0015f;
        private double angleStep = 0.5d;
        private Action<GPSPos> onLocationChanged;
        private bool locationMonitoring;

        public MockLocationService(ITimerService timerService)
        {
            timerService.AddListenerOnTimer(OnTimePassed, 2);
            OnlineMapsLocationService ls = OnlineMapsLocationService.instance;
            ls.OnLocationChanged += pos =>
            {
                hasInitialized = true;
                realPos = GPSPos.FromOnlineMapVector2(pos);
                UpdateCurrentPos();
                onLocationChanged.SafeInvoke(currentPos);
            };
            CommandHandlers.RegisterCommandHandlers(this);
        }

        /// <summary>
        /// We're rotating our mock point around the real position 
        /// </summary>
        private void UpdateCurrentPos()
        {
            float angle = (float)(currentAngleDegrees * (Math.PI / 180f)); // Convert to radians
            currentPos.lat = Mathf.Cos(angle) * radius + realPos.lat;
            currentPos.lon = Mathf.Sin(angle) * radius + realPos.lon;
        }

        private void OnTimePassed(int seconds)
        {
            if (locationMonitoring && hasInitialized)
            {
                currentAngleDegrees += angleStep * seconds;
                currentAngleDegrees %= 360;
                UpdateCurrentPos();
                onLocationChanged.SafeInvoke(currentPos);
            }
        }

        public GPSPos GetPosition()
        {
            return currentPos;
        }

        public void AddListenerOnLocationChanged(Action<GPSPos> action)
        {
            onLocationChanged += action;
        }

        public void Dispose()
        {
            CommandHandlers.UnregisterCommandHandlers(this);
        }
    
        [CommandHandler(Description = "Wroclaw is 51.11 17.046, London is 51.505 -0.1324 ")]
        private void GPSPosChange(float lat, float lon)
        {
            realPos.lon = lon;
            realPos.lat = lat;
        }

        internal void ChangeMockSpeedToNext(double[] allPossibleSpeedsMetersPerSecond)
        {
            bool switched = false;
            for(int i=0; i < allPossibleSpeedsMetersPerSecond.Length && switched == false; i++)
            {
                double iSpeed = allPossibleSpeedsMetersPerSecond[i];
                if (iSpeed > angleStep)
                {
                    switched = true;
                    angleStep = iSpeed;
                }
            }
            if (switched == false)
            {
                angleStep = allPossibleSpeedsMetersPerSecond[0];
            }
        }

        public void StartLocationMonioring()
        {
            locationMonitoring = true;
        }

        public void StopLocationMonitoring()
        {
            locationMonitoring = false;
        }

        public bool IsInitialized()
        {
            return hasInitialized;
        }
    }
}