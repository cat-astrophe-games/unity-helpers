using System;

namespace CatAstropheGames
{
    public static class DateTimeExtensions
    {
        public static DateTime Midnight(this DateTime dt)
        {
            return dt.Date;
        }

        public static string ToCAGDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
    }
}