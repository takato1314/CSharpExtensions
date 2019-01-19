using System;

namespace Reevo.Unbroken.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Please use DateTimeOffset.FromUnixTimeSeconds for frameworks from 4.6 onwards
        /// </summary>
        /// <param name="secondsOffset"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimeSeconds(this long secondsOffset)
        {
            return Epoch.AddSeconds(secondsOffset);
        }

        /// <summary>
        /// Please use DateTimeOffset.FromUnixTimeMilliseconds for frameworks from 4.6 onwards
        /// </summary>
        /// <param name="millisecondsOffset"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimeMilliseconds(this long millisecondsOffset)
        {
            return Epoch.AddMilliseconds(millisecondsOffset);
        }

        /// <summary>
        /// Please use DateTimeOffset.ToUnixTimeSeconds for frameworks from 4.6 onwards
        /// </summary>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTime date)
        {
            // TimeZoneInfo.ConvertTimeToUtc(dateTime) - Epoch
            return Convert.ToInt64((date.ToUniversalTime() - Epoch).TotalSeconds);
        }


        /// <summary>
        /// Please use DateTimeOffset.ToUnixTimeMilliseconds for frameworks from 4.6 onwards
        /// </summary>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTime date)
        {
            // TimeZoneInfo.ConvertTimeToUtc(dateTime) - Epoch
            return Convert.ToInt64((date.ToUniversalTime() - Epoch).TotalMilliseconds);
        }
    }
}
