namespace WeatherService.Dtos
{
    public class CurrentWeatherResource
    {
        public float Lat { get; set; }
        public float Lon { get; set; }
        public Current Current { get; set; }
    }

    public class Current
    {
        public long Dt { get; set; }
        public float Temp { get; set; }
        public float FeelsLike { get; set; }
        public float Humidity {get;set;}
        public float WindSpeed {get;set;}
    }
}