using System.ComponentModel.DataAnnotations;

namespace WeatherService.Models
{
    public class GeoCoord
    {
        /// <summary>
        /// Latitude of the geographic coordinate
        /// </summary>
        /// <example>37.8136</example>
        [Required]
        [Range(-90, 90)]
        public float? Lat { get; set; }

        /// <summary>
        /// Longtitude of the geographic coordinate
        /// </summary>
        /// <example>144.9631</example>
        [Required]
        [Range(-180, 180)]
        public float? Lon { get; set; }
    }
}