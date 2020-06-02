using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatAstropheGames
{
    public class LocationService : ILocationService
    {
        private ITimerService timerService;

        private GPSPos? lastPos;
        private bool isMonitoring;
        private List<Action<GPSPos>> listeners = new List<Action<GPSPos>>();

        public LocationService(ITimerService timerService)
        {
            this.timerService = timerService;
        }

        public GPSPos GetPosition()
        {
            if (!IsInitialized())
            {
                throw new Exception("GPS is not initialized");
            }
            return GPSPos.FromLocationInfo(Input.location.lastData);
        }

        public void AddListenerOnLocationChanged(Action<GPSPos> action)
        {
            listeners.Add(action);
        }

        public void StartLocationMonioring()
        {
            isMonitoring = true;
            timerService.AddListenerOnTimer(TimePassed, 2);
            if (!Permission.HasUserAuthorizedPermission(PermissionName.LocationWhenInUse))
            {
                Debug.Log("User didn not authorize GPS permission, asking.");
                Permission.RequestUserPermission(PermissionName.LocationWhenInUse);
            }
            else
            {
                Input.location.Start();
            }
        }

        private void TimePassed(int seconds)
        {
            if (isMonitoring)
            {
                if (Input.location.status == LocationServiceStatus.Stopped)
                {
                    Debug.Log("Location status is stopped, starting.");
                    Input.location.Start();
                }
                if (Input.location.status == LocationServiceStatus.Running)
                {
                    GPSPos newPos = GetPosition();
                    if (lastPos == null || !lastPos.Value.IsEqual(newPos))
                    {
                        NotifyListeners(newPos);
                    }
                    lastPos = newPos;
                }
            }
        }

        private void NotifyListeners(GPSPos newPos)
        {
            foreach (Action<GPSPos> listener in listeners)
            {
                listener.SafeInvoke(newPos);
            }
        }

        public void StopLocationMonitoring()
        {
            isMonitoring = false;
            timerService.RemoveListenerOnTimer(TimePassed);
            Input.location.Stop();
        }

        public void Dispose()
        {
            if (isMonitoring)
            {
                StopLocationMonitoring();
            }
        }

        public bool IsInitialized()
        {
            return Permission.HasUserAuthorizedPermission(PermissionName.LocationWhenInUse) && Input.location.status == LocationServiceStatus.Running;
        }
    }
}