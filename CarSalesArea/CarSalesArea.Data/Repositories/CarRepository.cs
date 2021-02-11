using CarSalesArea.Data.Helpers;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CarSalesArea.Data.Repositories
{
    public class CarRepository: BaseRepository, ICarRepository
    {
        private static Lazy<string> GetAllCars= ScriptLoader.GetLazyEmbeddedResource<CarEntity>();
        private static Lazy<string> GetCarById = ScriptLoader.GetLazyEmbeddedResource<CarEntity>();
        private static Lazy<string> GetLatestCarId = ScriptLoader.GetLazyEmbeddedResource<CarEntity>();
        private static Lazy<string> CreateCar = ScriptLoader.GetLazyEmbeddedResource<CarEntity>();
        private static Lazy<string> UpdateCar = ScriptLoader.GetLazyEmbeddedResource<CarEntity>();
        private static Lazy<string> DeleteCar = ScriptLoader.GetLazyEmbeddedResource<CarEntity>();

        public CarRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<CarEntity> GetCarByIdAsync(long id)
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<CarEntity, SalesArea, FuelTypeEntity, CarEntity>(
                    GetCarById.Value,
                    (car, area, fuel) =>
                    {
                        car.SalesArea = area;
                        car.FuelType = fuel;
                        return car;
                    },
                    new
                    {
                        Id = id
                    },
                    splitOn: "AreaId, FuelTypeId")).FirstOrDefault();
            });
        }

        public async Task<IEnumerable<CarEntity>> GetAllCarsCollectionAsync()
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<CarEntity, SalesArea, FuelTypeEntity, CarEntity>(
                    GetAllCars.Value,
                    (car, area, fuel) =>
                    {
                        car.SalesArea = area;
                        car.FuelType = fuel;
                        return car;
                    },
                    splitOn: "AreaId, FuelTypeId"));
            });
        }

        public async Task<long> CreateManagerAsync(CarEntity carEntity)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    CreateCar.Value,
                    new
                    {
                        carEntity.Brand,
                        carEntity.Model,
                        carEntity.Year,
                        carEntity.EngineVolume,
                        carEntity.Mileage,
                        carEntity.Description,
                        carEntity.Price,
                        carEntity.VinCode,
                        carEntity.Color,
                        carEntity.Body,
                        AreaId = carEntity.SalesArea.Id,
                        FuelTypeId = carEntity.FuelType.FuelType,
                    });

                await conn.ExecuteAsync(command);
            });

            return await GetLatestCarIdAsync(await GetConnection());
        }

        public async Task UpdateCarAsync(CarEntity carEntity)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    UpdateCar.Value,
                    new
                    {
                        carEntity.Id,
                        carEntity.Brand,
                        carEntity.Model,
                        carEntity.Year,
                        carEntity.EngineVolume,
                        carEntity.Mileage,
                        carEntity.Description,
                        carEntity.Price,
                        carEntity.VinCode,
                        carEntity.Color,
                        carEntity.Body,
                        AreaId = carEntity.SalesArea.Id,
                        FuelTypeId = carEntity.FuelType.FuelType,
                    });

                await conn.ExecuteAsync(command);
            });
        }

        public async Task DeleteCarAsync(long id)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    DeleteCar.Value,
                    new
                    {
                        Id = id
                    });

                await conn.ExecuteAsync(command);
            });
        }

        private async Task<long> GetLatestCarIdAsync(IDbConnection connection)
        {
            var command = new CommandDefinition(
                GetLatestCarId.Value);

            var result = await connection.QueryFirstOrDefaultAsync<long>(command);

            connection.Close();

            return result;
        }
    }
}
