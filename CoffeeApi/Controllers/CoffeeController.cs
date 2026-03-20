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
        public async Task<IActionResult> BrewCoffee()
        {
            var result = await _coffeeService.BrewCoffee();

            if (result.StatusCode != 200)
            {
                return StatusCode(result.StatusCode);
            }

            return Ok(result.Response);
        }
    }
}
