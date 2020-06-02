

using UnityEngine;

namespace CatAstropheGames
{
    public static class ColorExtensions
    {
        /**
         * converting to color from string: "rgba(int<0-255>, int<0-255>, int<0-255>, int<0-255>)
         */
        public static Color FromRGBA(string s)
        {
            string[] colorParts = s.Split(',');
            colorParts[0] = colorParts[0].Substring("rgba(".Length);
            colorParts[3] = colorParts[3].Split(')')[0];

            Color color = new Color(
                float.Parse(colorParts[0]) / (float)255,
                float.Parse(colorParts[1]) / (float)255,
                float.Parse(colorParts[2]) / (float)255,
                float.Parse(colorParts[3]) / (float)255);

            return color;
        }

        public static UnityEngine.Color Random()
        {
            UnityEngine.Color[] colors = new[] { UnityEngine.Color.black, UnityEngine.Color.green, UnityEngine.Color.yellow, UnityEngine.Color.gray, UnityEngine.Color.blue, UnityEngine.Color.red, UnityEngine.Color.cyan };

            return colors[UnityEngine.Random.Range(0, colors.Length - 1)];
        }

        public static UnityEngine.Color FromRGB(string s)
        {
            string[] colorParts = s.Split(',');
            colorParts[0] = colorParts[0].Substring("rgb(".Length);
            colorParts[2] = colorParts[2].Split(')')[0];

            UnityEngine.Color color = new UnityEngine.Color(
                float.Parse(colorParts[0]) / (float)255,
                float.Parse(colorParts[1]) / (float)255,
                float.Parse(colorParts[2]) / (float)255);

            return color;
        }
    }


}