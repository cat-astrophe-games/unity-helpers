using UnityEngine;
using System.Collections;

namespace CatAstropheGames
{
    public static class IntArrayExtensions
    {
        public static int Random(this int[] array)
        {
            int random = UnityEngine.Random.Range(0, array.Length - 1);
            return array[random];
        }
    }
}