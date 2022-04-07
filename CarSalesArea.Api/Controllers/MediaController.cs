using AutoMapper;
using CarSalesArea.Api.ViewModels;
using CarSalesArea.Core.Infrastructure;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarSalesArea.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MediaController:ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public MediaController(IMediaService mediaService,
            IStorageService storageService,
            IMapper mapper)
        {
            _mediaService = mediaService;
            _storageService = storageService;
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


        // POST /api/Files
        // Called once for each file uploaded.
        [HttpPost()]
        public async Task<IActionResult> Upload(/*[FromForm]JObject data IFormFile file*/)
        {
            var file = Request.Form.Files.FirstOrDefault();

            //IFormFile file = data["carMediaFile"].ToObject<IFormFile>();
            // IFormFile.FileName is untrustworthy user input, and we're
            // using it for both blob names and for display on the page,
            // so we aggressively sanitize. In a real app, we'd probably
            // do something more complex and robust for handling filenames.
            //var name = SanitizeFilename(file.FileName);

            //if (String.IsNullOrWhiteSpace(name))
            //{
            //    throw new ArgumentException();
            //}

            using (Stream stream = file.OpenReadStream())
            {
                //await storage.Save(stream, name);
                await _storageService.Save(stream, file.FileName);
            }

            return Accepted();
        }

        //// GET /api/Files/{filename}
        //// Called when clicking a link to download a specific file.
        //[HttpGet("{filename}")]
        //public async Task<IActionResult> Download(string filename)
        //{
        //    var stream = await storage.Load(filename);

        //    // This usage of File() always triggers the browser to perform a file download.
        //    // We always use "application/octet-stream" as the content type because we don't record
        //    // any information about content type from the user when they upload a file.
        //    return File(stream, "application/octet-stream", filename);
        //}

        //private static string SanitizeFilename(string filename)
        //{
        //    var sanitizedFilename = filenameRegex.Replace(filename, "").TrimEnd('.');

        //    if (sanitizedFilename.Length > MaxFilenameLength)
        //    {
        //        sanitizedFilename = sanitizedFilename.Substring(0, MaxFilenameLength);
        //    }

        //    return sanitizedFilename;
        //}
    }
}
