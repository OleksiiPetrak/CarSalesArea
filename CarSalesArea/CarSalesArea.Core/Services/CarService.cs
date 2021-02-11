﻿using AutoMapper;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services
{
    public class CarService: ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CarModel> GetCarByIdAsync(long id)
        {
            var carEntity = await _carRepository.GetCarByIdAsync(id);
            var carModel = _mapper.Map<CarModel>(carEntity);

            return carModel;
        }

        public async Task<IEnumerable<CarModel>> GetAllCarsAsync()
        {
            var carEntityCollection = await _carRepository.GetAllCarsCollectionAsync();
            var carModelCollection = _mapper.Map<IEnumerable<CarModel>>(carEntityCollection);

            return carModelCollection;
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
