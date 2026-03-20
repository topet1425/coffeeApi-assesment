using System.Text.Json;

namespace CoffeeApi.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WeatherService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<double> GetTemperatureAsync()
        {
            var apiKey = _config["Weather:ApiKey"];
            var city = _config["Weather:City"]; // e.g. Manila

            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Weather API failed");

            var content = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(content);
            var temp = doc.RootElement
                          .GetProperty("main")
                          .GetProperty("temp")
                          .GetDouble();

            return temp;
        }
    }
}
