using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSalesArea.Core.Infrastructure;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesArea.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CarController: ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("cars", Name = nameof(GetAllCarsAsync))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetAllCarsAsync()
        {
            var carCollection = await _carService.GetAllCarsAsync();

            return Ok(carCollection);
        }

        [HttpGet("{id}", Name = nameof(GetCarByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(304)]
        [ResponseCache(Duration = 300)]
        [Etag]
        public async Task<ActionResult<CarModel>> GetCarByIdAsync(long id)
        {
            var car = await _carService.GetCarByIdAsync(id);

            if (!Request.GetEtagHandler().NoneMatch(car))
            {
                return StatusCode(304, car);
            }

            return Ok(car);
        }

        [HttpPost("car", Name = nameof(CreateCarAsync))]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateCarAsync([FromBody] CarModel car)
        {
            var carId = await _carService.CreateCarAsync(car);

            var link = Url.Link(nameof(GetCarByIdAsync),
                new { id = carId});

            return Created(link, null);
        }

        [HttpPut("{id}", Name = nameof(UpdateCarAsync))]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateCarAsync(long id, [FromBody] CarModel car)
        {
            car.Id = id;
            await _carService.UpdateCarAsync(car);

            return Ok();
        }

        [HttpDelete("{id}", Name = nameof(DeleteCarAsync))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteCarAsync(long id)
        {
            await _carService.RemoveCarAsync(id);

            return NoContent();
        }
    }
}
