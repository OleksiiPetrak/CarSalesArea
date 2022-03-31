using AutoMapper;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services
{
    public class MediaService: IMediaService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IMapper _mapper;

        public MediaService(IMediaRepository mediaRepository, IMapper mapper)
        {
            _mediaRepository = mediaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhotoModel>> GetPhotoCollectionByCarIdAsync(long carId)
        {
            var photoEntities = await _mediaRepository.GetPhotoCollectionByCarIdAsync(carId);
            var photoModels = _mapper.Map<IEnumerable<PhotoModel>>(photoEntities);

            return photoModels;
        }

        public async Task<IEnumerable<PhotoModel>> GetAllPhotosCollectionAsync()
        {
            var photoEntities = await _mediaRepository.GetAllPhotosCollectionAsync();
            var photoModels = _mapper.Map<IEnumerable<PhotoModel>>(photoEntities);

            return photoModels;
        }

        public async Task<long> CreatePhotoAsync(PhotoModel photoModel)
        {
            var photoEntity = _mapper.Map<PhotoEntity>(photoModel);
            var id = await _mediaRepository.CreatePhotoAsync(photoEntity);

            return id;
        }

        public async Task UpdatePhotoAsync(PhotoModel photoModel)
        {
            var photoEntity = _mapper.Map<PhotoEntity>(photoModel);
            await _mediaRepository.UpdatePhotoAsync(photoEntity);
        }

        public async Task DeletePhotoAsync(long id)
        {
            await _mediaRepository.DeletePhotoAsync(id);
        }
    }
}
