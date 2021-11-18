using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherService.Models;
using WeatherService.Dtos;
using WeatherService.Core;

namespace WeatherService.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<WeatherController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public WeatherController(IHttpClientFactory httpClientFactory, ILogger<WeatherController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = configuration;
        }

        [HttpGet]
        [Route("Current")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CurrentWeatherResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] GeoCoord geoCoord)
        {
            string key = _config["OpenWeatherMapKey"];
            string url = $"onecall?lat={geoCoord.Lat}&lon={geoCoord.Lon}&appid={key}&exclude=minutely,hourly,alerts,daily&units=metric";

            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient(Constants.OpenWeatherMap);
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
                };

                CurrentWeatherResource resource = await httpClient.GetFromJsonAsync<CurrentWeatherResource>(url, options);
                DateTime dt = DateTimeOffset.FromUnixTimeSeconds(resource.Current.Dt).DateTime;

                CurrentWeatherResponse response = new CurrentWeatherResponse()
                {
                    Timestamp = DateTimeUtils.ToIsoString(dt),
                    Coord = new GeoCoord
                    {
                        Lat = resource.Lat,
                        Lon = resource.Lon
                    },
                    Temp = new CurrentTemperature()
                    {
                        FeelsLikeC = resource.Current.FeelsLike,
                        TempC = resource.Current.Temp
                    },
                    Humidity = resource.Current.Humidity,
                    WindSpeedMps = resource.Current.WindSpeed
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Request failed - {url}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}