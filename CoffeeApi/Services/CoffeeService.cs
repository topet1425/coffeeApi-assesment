using CoffeeApi.Models;
using System.Globalization;

namespace CoffeeApi.Services
{
    public class CoffeeService : ICoffeeService
    {
        private static int _counter = 0;
        private static readonly object _lock = new();

        public (int StatusCode, CoffeeResponse? Response) BrewCoffee(DateTimeOffset dateTimeOffset)
        {

            // Requirement #3 (April 1st)
            if (dateTimeOffset.Month == 4 && dateTimeOffset.Day == 1)
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

            var response = new CoffeeResponse
            {
                Message = "Your piping hot coffee is ready",
                Prepared = dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture)
            };

            return (200, response);
        }
    }
}
