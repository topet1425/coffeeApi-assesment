using CoffeeApi.Models;

namespace CoffeeApi.Services
{
    public interface ICoffeeService
    {
        (int StatusCode, CoffeeResponse? Response) BrewCoffee(DateTimeOffset dateTimeOffset);
    }
}
