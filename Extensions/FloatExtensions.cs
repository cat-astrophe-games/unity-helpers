using UnityEngine;
using System.Collections;
using System;

namespace CatAstropheGames
{
    public static class FloatExtensions
    {
        private static string MilesToFraction(this float milesFloat)
        {
            const int fractionsCount = 8;
            bool IsMileFraction(int Frac)
            {
                const float epsilon = 0.01f; //in miles
                int mileParts = Mathf.RoundToInt(milesFloat * (float)Frac);
                return (Mathf.Abs((float)Frac * milesFloat - (float)mileParts) < epsilon) && (mileParts != 0);
            }
            string MileFraction(int Frac)
            {
                int mileParts = Mathf.RoundToInt(milesFloat * (float)Frac);
                if (Frac == 1)
                {
                    return mileParts.ToString();
                }
                else
                {
                    return mileParts.ToString() + "/" + Frac.ToString();
                }
            }
            //First try some nice fractions of a mile: 1/2, 1/3, 1/4 ... 1/8
            for (int i = 1; i <= fractionsCount; i++)
            {
                if (IsMileFraction(i))
                {
                    return MileFraction(i);
                }
            }
            return "";
        }
        private static string MilesToFeet(this float milesFloat)
        {
            const int roundDiv = 100;
            const float desiredAccuracy = 0.05f; //5%
            //1 mile = 5280 feet
            int intFeet = Mathf.RoundToInt(milesFloat * 5280f); //Using "1 mile and 300 feet" form would be hacky here, so leaving it off for now to be compatible with ActivityTypeInCurrentChallenge
            int intFeetRounded = Mathf.RoundToInt(intFeet / (float)roundDiv) * roundDiv;
            if (Mathf.Abs(((float)intFeetRounded - (float)intFeet) / (float)intFeet) < desiredAccuracy)
            {
                return intFeetRounded.ToString();
            }
            else
            {
                return intFeet.ToString(); //nothing we can do here, just return the accurate value
            }
        }
        public static string MilesToNumber(this float milesFloat)
        {
            if (milesFloat < 0.002)
            {
                throw new Exception("Trying to convert almost-zero or negative distance to human-readable miles"); //obviously our algorithm will fail in this case, which is incompatible with current UIs
            }
            string milesFraction = MilesToFraction(milesFloat);
            if (milesFraction != "")
            {
                return milesFraction;
            }
            else
            {
                return MilesToFeet(milesFloat);
            }
        }
        public static string MilesUnits(this float milesFloat)
        {
            if (milesFloat < 0.002)
            {
                throw new Exception("Trying to convert almost-zero or negative distance to human-readable miles");
            }
            //this is ugly, but that's the easiest way to do the check, and this routine is called rarely, so efficiency is not an issue here
            string milesFraction = MilesToFraction(milesFloat);
            if (milesFraction != "")
            {
                if (milesFloat < 1.05f)
                {
                    return "mile"; //I couldn't find a proper convention on that, following the most "usual" one: if <= 1 then "mile" else "miles"
                }
                else
                {
                    return "miles";
                }
            }
            else
            {
                return "feet";
            }
        }
        public static string AsHumanReadableMilesAndFeet(this float milesFloat)
        {
            return MilesToNumber(milesFloat) + " " + MilesUnits(milesFloat);
        }
    }
}