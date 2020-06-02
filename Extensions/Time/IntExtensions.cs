using UnityEngine;
using System.Collections;
using System;

namespace CatAstropheGames
{
    public static class IntExtensions
    {
        public static string SecondsAsHumanReadableTime(this int seconds, TimeFormat format = TimeFormat.FullWord)
        {
            string built = "";

            int minutes = seconds / 60;

            if (minutes > 0)
            {
                built += $"{minutes}{GetMinutes(minutes, format)}";
            }

            int secondsRest = seconds % 60;

            if (minutes > 0 && secondsRest > 0)
            {
                built += " ";
            }


            if (secondsRest > 0)
            {
                built += $"{secondsRest}{GetSeconds(secondsRest, format)}";
            }

            return built;
        }

        private static string GetMinutes(int minutes, TimeFormat format)
        {
            switch (format)
            {
                case TimeFormat.FullWord:
                    return minutes > 1 ? " minutes" : " minute";
                case TimeFormat.Short:
                    return " min";
                case TimeFormat.OnlyLetter:
                    return "m";
                default:
                    throw new Exception("Unsupported format: " + format);
            }
        }

        private static string GetSeconds(int seconds, TimeFormat format)
        {
            switch (format)
            {
                case TimeFormat.FullWord:
                    return seconds > 1 ? " seconds" : " second";
                case TimeFormat.Short:
                    return " sec";
                case TimeFormat.OnlyLetter:
                    return "s";
                default:
                    throw new Exception("Unsupported format: " + format);
            }
        }
    }
}