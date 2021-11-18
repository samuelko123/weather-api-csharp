using System.ComponentModel.DataAnnotations;

namespace WeatherService.Models
{
    public class CurrentTemperature
    {
        /// <summary>
        /// Temperature (in Celcius)
        /// </summary>
        /// <example>12.89</example>
        [Required]
        public float TempC { get; set; }

        /// <summary>
        /// Temperature (in Fahrenheit)
        /// </summary>
        /// <example>55.2</example>
        [Required]
        public float TempF
        {
            get
            {
                return CToF(TempC);
            }
        }

        /// <summary>
        /// "Feels Like" Temperature (in Celcius)
        /// </summary>
        /// <example>11.7</example>
        [Required]
        public float FeelsLikeC { get; set; }

        /// <summary>
        /// "Feels Like" Temperature (in Fahrenheit)
        /// </summary>
        /// <example>53.06</example>
        [Required]
        public float FeelsLikeF
        {
            get
            {
                return CToF(FeelsLikeC);
            }
        }

        private float CToF(float c)
        {
            return c * 9 / 5 + 32;
        }
    }
}