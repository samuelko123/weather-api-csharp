using System.ComponentModel.DataAnnotations;
using WeatherService.Models;

namespace WeatherService.Dtos
{
    public class CurrentWeatherResponse
    {
        /// <summary>
        /// Timestamp of the weather data (in ISO string)
        /// </summary>
        /// <example>2000-01-01T00:00:00Z</example>
        [Required]
        public string Timestamp { get; set; }

        [Required]
        public GeoCoord Coord { get; set; }

        [Required]
        public CurrentTemperature Temp { get; set; }

        /// <summary>
        /// Humidity (in %)
        /// </summary>
        /// <example>63.1</example>
        [Required]
        public float Humidity { get; set; }

        /// <summary>
        /// Wind Speed (in m/s)
        /// </summary>
        /// <example>63.1</example>
        [Required]
        public float WindSpeedMps { get; set; }
    }
}