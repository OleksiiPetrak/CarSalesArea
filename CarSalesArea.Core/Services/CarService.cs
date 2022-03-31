using AutoMapper;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagingOptions = CarSalesArea.Core.Models.PagingOptions;

namespace CarSalesArea.Core.Services
{
    public class CarService: ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMediaRepository _mediaRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IMapper mapper, IMediaRepository mediaRepository)
        {
            _carRepository = carRepository; 
            _mediaRepository = mediaRepository;
            _mapper = mapper;
        }

        public async Task<CarModel> GetCarByIdAsync(long id)
        {
            var carEntity = await _carRepository.GetCarByIdAsync(id);
            if (carEntity == null)
            {
                return new CarModel();
            }

            var photoEntityCollecion = await _mediaRepository.GetPhotoCollectionByCarIdAsync(carEntity.Id);
            carEntity.Photos = photoEntityCollecion;

            var carModel = _mapper.Map<CarModel>(carEntity);

            return carModel;
        }

        public async Task<PagedResults<CarModel>> GetAllCarsAsync(PagingOptions pagingOptions)
        {
            var pagingOptionsEntity = _mapper.Map<Data.Models.PagingOptions>(pagingOptions);
            var carEntityCollection = await _carRepository.GetAllCarsCollectionAsync(pagingOptionsEntity);

            foreach (var carEntity in carEntityCollection)
            {
                carEntity.Photos = await _mediaRepository.GetPhotoCollectionByCarIdAsync(carEntity.Id);
            }

            var carModelCollection = _mapper.Map<IEnumerable<CarModel>>(carEntityCollection);

            var pagedCars = carModelCollection
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);

            return new PagedResults<CarModel>()
            {
                Items = pagedCars,
                TotalSize = carModelCollection.Count()
            };
        }

        public async Task<long> CreateCarAsync(CarModel car)
        {
            var carEntity = _mapper.Map<CarEntity>(car);
            var createdCarId = await _carRepository.CreateManagerAsync(carEntity);

            return createdCarId;
        }

        public async Task UpdateCarAsync(CarModel car)
        {
            var carEntity = _mapper.Map<CarEntity>(car);
            await _carRepository.UpdateCarAsync(carEntity);
        }

        public async Task RemoveCarAsync(long id)
        {
            await _carRepository.DeleteCarAsync(id);
        }
    }
}
