using CoffeeApi.Services;

namespace CoffeeApi.Test.Fakes
{
    public class FakeWeatherService : IWeatherService
    {
        public Task<double> GetTemperatureAsync() => Task.FromResult(25.0);
    }
}
