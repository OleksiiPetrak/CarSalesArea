using AutoMapper;
using CarSalesArea.Api.ViewModels;
using CarSalesArea.Core.Infrastructure;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSalesArea.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MediaController:ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;

        public MediaController(IMediaService mediaService, IMapper mapper)
        {
            _mediaService = mediaService;
            _mapper = mapper;
        }

        [HttpGet("photos", Name = nameof(GetAllPhotoAsync))]
        [ProducesResponseType(200)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public async Task<ActionResult<Collection<PhotoViewModel>>> GetAllPhotoAsync()
        {
            var photoModels = await _mediaService.GetAllPhotosCollectionAsync();

            var photos = _mapper.Map<IEnumerable<PhotoViewModel>>(photoModels);

            var collection = new Collection<PhotoViewModel>()
            {
                Self = Link.ToCollection(nameof(GetAllPhotoAsync)),
                Value = photos.ToArray()
            };

            return Ok(collection);
        }

        [HttpGet("{carId}/photos", Name = nameof(GetPhotosByCarId))]
        [ProducesResponseType(200)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public async Task<ActionResult<Collection<PhotoViewModel>>> GetPhotosByCarId(long carId)
        {
            var photoModels = await _mediaService.GetPhotoCollectionByCarIdAsync(carId);

            var photos = _mapper.Map<IEnumerable<PhotoViewModel>>(photoModels);

            var collection = new Collection<PhotoViewModel>()
            {
                Self = Link.ToCollection(nameof(GetAllPhotoAsync)),
                Value = photos.ToArray()
            };

            return Ok(collection);
        }

        [HttpPost("{carId}/photo", Name = nameof(CreatePhotoAsync))]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreatePhotoAsync(
            long carId,
            [FromBody] PhotoViewModel photo)
        {
            photo.Car = new CarViewModel() {Id = carId}; 
            var photoModel = _mapper.Map<PhotoModel>(photo);

            var photoPath = await _mediaService.CreatePhotoAsync(photoModel);

            var link = Url.Link(nameof(CarController.GetCarByIdAsync),
                new {id = photo.Car.Id});

            return Created(
                link,
                null);
        }

        [HttpDelete("{id}", Name = nameof(DeletePhotoAsync))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeletePhotoAsync(long id)
        {
            await _mediaService.DeletePhotoAsync(id);

            return NoContent();
        }

        [HttpPut("{id}", Name = nameof(UpdatePhotoAsync))]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdatePhotoAsync(
            long id,
            [FromBody] PhotoViewModel photo)
        {
            var photoModel = _mapper.Map<PhotoModel>(photo);
            photoModel.Id = id;
            await _mediaService.UpdatePhotoAsync(photoModel);

            return Ok();
        }
    }
}
