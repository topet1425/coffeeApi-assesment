using CoffeeApi.Models;
using CoffeeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeService _coffeeService;

        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        [HttpGet("/brew-coffee")]
        public IActionResult BrewCoffee()
        {
            var now = DateTimeOffset.Now;
            var result = _coffeeService.BrewCoffee(now);

            if (result.StatusCode != 200)
            {
                return StatusCode(result.StatusCode);
            }

            return Ok(result.Response);
        }
    }
}
