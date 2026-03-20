namespace CoffeeApi.Services
{
    public interface IWeatherService
    {
        Task<double> GetTemperatureAsync();
    }
}
