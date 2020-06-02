using UnityEngine;
using System.Collections;
using System;

namespace CatAstropheGames
{
    public static class ConversionUtils 
    {
        public static float MetersToMiles(int meters)
        {
            // 1 metre = 0.000621371 mile
            return meters * 0.000621371f;
        }
    }

}