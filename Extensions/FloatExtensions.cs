using UnityEngine;
using System.Collections;
using System;

namespace CatAstropheGames
{
    public static class FloatExtensions
    {
        public static string AsHumanReadableMilesAndFeet(this float milesFloat)
        {
            int miles = (int)milesFloat;
            //1 mile = 5280 feet
            int feet = (int)((milesFloat - (int)milesFloat) * 5280);

            if (Math.Abs(Mathf.RoundToInt(milesFloat) - milesFloat) < 0.1f)
            {
                miles = Mathf.RoundToInt(milesFloat);
                feet = 0;
            }

            string resultText = "";
            if (miles > 0)
            {
                string milesText = "miles";
                if (miles == 1)
                {
                    milesText = "mile";
                }
                resultText += " " + miles + " " + milesText;
            }
            if (miles > 0 & feet > 0)
            {
                resultText += " and";
            }
            if (feet > 0)
            {
                resultText += " " + feet + " feet";
            }
            return resultText;
        }
    }
}