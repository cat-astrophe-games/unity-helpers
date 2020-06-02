using UnityEngine;
using System.Collections;

namespace CatAstropheGames
{
    public static class ULongExtensions
    {
        public static string MSecAsHumanReadableTime(this ulong millisecondsIn)
        {
            return ((int)(millisecondsIn / 1000)).SecondsAsHumanReadableTime();
        }
    }
}