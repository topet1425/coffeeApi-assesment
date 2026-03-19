using CoffeeApi.Services;
namespace CoffeeApi.Tests
{
    public class CoffeeServiceTests
    {
        [Fact]
        public void Should_Return_503_On_5th_Call()
        {
            var now = DateTimeOffset.Now;
            var service = new CoffeeService();

            for (int i = 1; i <= 4; i++)
            {
                var result = service.BrewCoffee(now);
                Assert.Equal(200, result.StatusCode);
            }

            var coffeeResponse = service.BrewCoffee(now);
            Assert.Equal(503, coffeeResponse.StatusCode);
        }

        [Fact]
        public void Should_Return_418_On_April_1st()
        {
            var mockDate = new DateTimeOffset(2026, 4, 1, 10, 0, 0, TimeSpan.Zero);
            var service = new CoffeeService();
            var coffeeResponse = service.BrewCoffee(mockDate);

            Assert.Equal(418, coffeeResponse.StatusCode);
        }

    }
}
