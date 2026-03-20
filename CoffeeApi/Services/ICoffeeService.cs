using CoffeeApi.Models;

namespace CoffeeApi.Services
{
    public interface ICoffeeService
    {
        Task<(int StatusCode, CoffeeResponse? Response)> BrewCoffee();
    }
}
