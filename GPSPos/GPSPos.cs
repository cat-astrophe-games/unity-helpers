using System;
using UnityEngine;

namespace CatAstropheGames
{
    /// <summary>
    /// Reasoning behind creating this instead of using Vector2
    /// is that you will always know which one is latitude and which is longitude
    ///
    /// Wroclaw, Poland is lat 51, lon 17
    /// If you type `51.11 17.11` in google maps, then you will get wroclaw location.
    /// So the default recommended order is lat, lon.
    /// testing: https://www.latlong.net/
    /// </summary>
    public struct GPSPos
    {
        public static readonly GPSPos Zero = new GPSPos(0, 0);
        
        public double lat, lon;

        public GPSPos(double lat, double lon)
        {
            this.lat = lat;
            this.lon = lon;
        }
        
        public static GPSPos FromOnlineMapVector2(Vector2 pos)
        {
            // here you can see that they use different order. x as lon, y as lat.
            return new GPSPos(pos.y, pos.x);
        }

        public static GPSPos FromLatLog(double latitude, double longitude)
        {
            return new GPSPos(latitude, longitude);
        }

        public override string ToString()
        {
            return $"lat: {lat:#.######}, lon {lon:#.######}";
        }

        public Vector2 ToOnlineMapsVector2()
        {
            //lon lat
            return new Vector2((float)lon, (float)lat);
        }

        internal static GPSPos FromLocationInfo(LocationInfo lastData)
        {
            return new GPSPos(lastData.latitude, lastData.longitude);
        }

        public GPSDistance Distance(GPSPos pos2)
        {
            return new GPSDistance(this, pos2);
        }

        public bool IsEqual(GPSPos second)
        {
            return lon == second.lon && lat == second.lat;
        }
    }
}