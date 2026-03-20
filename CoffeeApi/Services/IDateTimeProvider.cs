namespace CoffeeApi.Services
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
    }
}
