using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CatAstropheGames
{
    public static class Vector2dList
    {
        public static List<Vector2> CalculateBounds(this List<OnlineMapsVector2d> points)
        {
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2(float.MinValue, float.MinValue);

            foreach (Vector2 point in points)
            {
                if (point.x < min.x)
                {
                    min.x = point.x;
                }
                if (point.y < min.y)
                {
                    min.y = point.y;
                }
                if (point.x > max.x)
                {
                    max.x = point.x;
                }
                if (point.y > max.y)
                {
                    max.y = point.y;
                }
            }

            return new List<Vector2>() { min, max };
        }
    }
}