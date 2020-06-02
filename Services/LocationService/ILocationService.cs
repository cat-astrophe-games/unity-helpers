using System;
using UnityEngine;

namespace CatAstropheGames
{
    /// <summary>
    /// This could possibly be removed, because there is UnityEngine.LocationSEevice
    /// </summary>
    public interface ILocationService
    {
        GPSPos GetPosition();
        void AddListenerOnLocationChanged(Action<GPSPos> action);
        void Dispose();
        void StartLocationMonioring();
        void StopLocationMonitoring();
        bool IsInitialized();
    }
}