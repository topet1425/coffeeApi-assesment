using CoffeeApi.Services;
using Moq;
namespace CoffeeApi.Tests
{
    public class CoffeeServiceTests
    {
        private CoffeeService CreateService(double temp = 25, DateTimeOffset? date = null)
        {
            var mockWeather = new Mock<IWeatherService>();
            mockWeather.Setup(x => x.GetTemperatureAsync()).ReturnsAsync(temp);

            var mockDate = new Mock<IDateTimeProvider>();
            mockDate.Setup(x => x.Now).Returns(date ?? DateTimeOffset.Now);

            return new CoffeeService(mockDate.Object, mockWeather.Object);
        }

        [Fact]
        public async Task Should_Return_200_On_Normal_Call()
        {
            var service = CreateService();

            var result = await service.BrewCoffee();

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Response);
            Assert.Equal("Your piping hot coffee is ready", result.Response.Message);
        }

        [Fact]
        public async Task Should_Return_503_On_5th_Call()
        {
            
            var service = CreateService();

            for (int i = 1; i <= 4; i++)
            {
                var result = await service.BrewCoffee();
                Assert.Equal(200, result.StatusCode);
            }

            var coffeeResponse = await service.BrewCoffee();
            Assert.Equal(503, coffeeResponse.StatusCode);
        }

        [Fact]
        public async Task Should_Return_418_On_April_1st()
        {
            var mockDate = new DateTimeOffset(2026, 4, 1, 10, 0, 0, TimeSpan.Zero);
            var service = CreateService(25, mockDate);
            var coffeeResponse = await service.BrewCoffee();

            Assert.Equal(418, coffeeResponse.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_ISO8601_Date()
        {
            var service = CreateService();

            var result = await service.BrewCoffee();

            Assert.NotNull(result.Response);

            var parsed = DateTimeOffset.Parse(result.Response.Prepared);

            Assert.True(parsed <= DateTimeOffset.Now);
        }

    }
}
