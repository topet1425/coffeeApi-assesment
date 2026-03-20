using CoffeeApi.Models;
using System.Globalization;

namespace CoffeeApi.Services
{
    public class CoffeeService : ICoffeeService
    {
        private static int _counter = 0;
        private static readonly object _lock = new();

        private readonly IWeatherService _weatherService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CoffeeService(IDateTimeProvider dateTimeProvider, IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _dateTimeProvider = dateTimeProvider;
        }


        public async Task<(int StatusCode, CoffeeResponse? Response)> BrewCoffee()
        {
            var now = _dateTimeProvider.Now;

            // Requirement #3 (April 1st)
            if (now.Month == 4 && now.Day == 1)
            {
                return (418, null);
            }

            int currentCount;
            lock (_lock)
            {
                _counter++;
                currentCount = _counter;
            }

            // Requirement #2 (every 5th request)
            if (currentCount % 5 == 0)
            {
                return (503, null);
            }

            var temp = await _weatherService.GetTemperatureAsync();

            var message = temp > 30
                ? "Your refreshing iced coffee is ready"
                : "Your piping hot coffee is ready";

            var response = new CoffeeResponse
            {
                Message = message,
                Prepared = now.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture)
            };

            return (200, response);
        }
    }
}
