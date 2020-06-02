using System;
using UnityEngine;

namespace CatAstropheGames
{
    /// <summary>
    /// This can be verified with online tool: https://gps-coordinates.org/distance-between-coordinates.php
    /// </summary>
    public class GPSDistance
    {
        private readonly float distanceInMeters;

        public GPSDistance(GPSPos one, GPSPos two)
        {
            distanceInMeters = DistanceInMeters(one, two);
        }

        public double Feet
        {
            get
            {
                return distanceInMeters * 3.28084;
            }
        }

        public float Meters
        {
            get
            {
                return distanceInMeters;
            }
        }

        private static float DistanceInMeters(GPSPos pos1, GPSPos pos2)
        {
            int R = 6371;
            // Radius of the earth in km
            double dLat = Deg2rad(pos2.lat - pos1.lat);
            // deg2rad below
            double dLon = Deg2rad(pos2.lon - pos1.lon);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(Deg2rad(pos1.lat)) * Math.Cos(Deg2rad(pos2.lat)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            // Distance in m
            return (float)(d * 1000);
        }

        private static float DistanceInMeters2(GPSPos pos1, GPSPos pos2)
        {
            /* Cache latitudeToMRatio/longitudeToMRatio - the resulting accuracy is a few cm for WGS84 spheroid model
               note, we use only relative values, so we aren't pursing a perfect accuracy */
            float lat = (float)pos2.lat * Mathf.Deg2Rad;
            const float LatCoef0 = 111132.92f;
            const float LatCoef2 = -559.82f;
            const float LatCoef4 = 1.175f;
            const float LatCoef6 = -0.0023f;
            float latitudeToMRatio = LatCoef0 + LatCoef2 * Mathf.Cos(2.0f * lat) + LatCoef4 * Mathf.Cos(4.0f * lat) + LatCoef6 * Mathf.Cos(6.0f * lat);
            const float LngCoef1 = 111412.84f;
            const float LngCoef3 = 93.5f;
            const float LngCoef5 = 0.118f;
            float longitudeToMRatio = LngCoef1 * Mathf.Cos(1.0f * lat) + LngCoef3 * Mathf.Cos(3.0f * lat) + LngCoef5 * Mathf.Cos(5.0f * lat);
            float diffLng = ((float)pos1.lon - (float)pos2.lon) * longitudeToMRatio;
            float diffLat = ((float)pos1.lat - (float)pos2.lat) * latitudeToMRatio;
            float dist = Mathf.Sqrt(diffLng * diffLng + diffLat * diffLat);
            return dist;
        }

        private static double Deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}