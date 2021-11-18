using System;

namespace WeatherService.Core
{
    public static class DateTimeUtils
    {
        public static string ToIsoString(DateTime dt)
        {
            return dt.ToString("o", System.Globalization.CultureInfo.InvariantCulture).Split('.')[0] + 'Z';
        }
    }
}