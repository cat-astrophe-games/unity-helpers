using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace CatAstropheGames
{
    public static class StringExtensions
    {
        public static string FirstToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static char RandomLetter()
        {
            int num = UnityEngine.Random.Range(0, 26); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }

        public static string RandomText(int length)
        {
            string text = "";
            for (int i = 0; i < length; i++)
            {
                text += RandomLetter();
            }
            return text;
        }
    }

}