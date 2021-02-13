using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarSalesArea.Api.ViewModels;
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
        private readonly IMapper _mapper;

        public CarController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet("cars", Name = nameof(GetAllCarsAsync))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<CarViewModel>>> GetAllCarsAsync()
        {
            var cars = await _carService.GetAllCarsAsync();
            var carViewModels = _mapper.Map<IEnumerable<CarViewModel>>(cars);

            var collection = new Collection<CarViewModel>()
            {
                Self = Link.ToCollection(nameof(GetAllCarsAsync)),
                Value = carViewModels.ToArray()
            };

            return Ok(collection);
        }

        [HttpGet("{id}", Name = nameof(GetCarByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(304)]
        [ResponseCache(Duration = 300)]
        [Etag]
        public async Task<ActionResult<CarViewModel>> GetCarByIdAsync(long id)
        {
            var car = await _carService.GetCarByIdAsync(id);

            var carViewModel = _mapper.Map<CarViewModel>(car);

            if (!Request.GetEtagHandler().NoneMatch(carViewModel))
            {
                return StatusCode(304, carViewModel);
            }

            return Ok(carViewModel);
        }

        [HttpPost("car", Name = nameof(CreateCarAsync))]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateCarAsync([FromBody] CarViewModel car)
        {
            var carModel = _mapper.Map<CarModel>(car);
            var carId = await _carService.CreateCarAsync(carModel);

            var link = Url.Link(nameof(GetCarByIdAsync),
                new { id = carId});

            return Created(link, null);
        }

        [HttpPut("{id}", Name = nameof(UpdateCarAsync))]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateCarAsync(long id, [FromBody] CarViewModel car)
        {
            car.Id = id;
            var carModel = _mapper.Map<CarModel>(car);
            
            await _carService.UpdateCarAsync(carModel);

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
