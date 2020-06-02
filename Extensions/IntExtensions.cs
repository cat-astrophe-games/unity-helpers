using UnityEngine;
using System.Collections;

namespace CatAstropheGames
{
    public static class IntExtensions
    {
        public static string AsHumanReadabileTime(this int secondsIn)
        {
            string built = "";

            int minutes = secondsIn / 60;
            if (minutes > 0)
            {
                string minutesText = "minute";
                if (minutes > 1)
                {
                    minutesText = "minutes";
                }
                built += $"{minutes} {minutesText}";
            }

            int seconds = secondsIn % 60;

            if (minutes > 0 & seconds > 0)
            {
                built += " and ";
            }

            if (seconds > 0)
            {
                string secondsText = "second";
                if (seconds > 1)
                {
                    secondsText = "seconds";
                }
                built += $"{seconds} {secondsText}";
            }

            return built;
        }
    }
}