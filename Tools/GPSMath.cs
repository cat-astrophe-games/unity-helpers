using UnityEngine;
using System.Collections;
using System;


namespace CatAstropheGames
{
    public static class GPSMath
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns>distance in meters</returns>
        public static float GetDistanceFromLatLonInM(double lat1, double lon1, double lat2, double lon2)
        {
            int R = 6371;
            // Radius of the earth in km
            double dLat = Deg2rad(lat2 - lat1);
            // deg2rad below
            double dLon = Deg2rad(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            // Distance in m
            return (float)(d * 1000);
        }

        private static double Deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }

}