using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarSalesArea.Data.Helpers;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CarSalesArea.Data.Repositories
{
    public class CarRepository: BaseRepository, ICarRepository
    {
        private static Lazy<string> GetAllCars= ScriptLoader.GetLazyEmbeddedResource<Car>();
        private static Lazy<string> GetCarById = ScriptLoader.GetLazyEmbeddedResource<Car>();
        private static Lazy<string> GetLatestCarId = ScriptLoader.GetLazyEmbeddedResource<Car>();
        private static Lazy<string> CreateCar = ScriptLoader.GetLazyEmbeddedResource<Car>();
        private static Lazy<string> UpdateCar = ScriptLoader.GetLazyEmbeddedResource<Car>();
        private static Lazy<string> DeleteCar = ScriptLoader.GetLazyEmbeddedResource<Car>();

        public CarRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Car> GetCarByIdAsync(long id)
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<Car, SalesArea, FuelTypeEntity, PhotoEntity, Car>(
                    GetCarById.Value,
                    (car, area, fuel, photo) =>
                    {
                        car.SalesArea = area;
                        car.FuelType = fuel;
                        car.PhotoEntityPath = photo;
                        return car;
                    },
                    new
                    {
                        Id = id
                    },
                    splitOn: "AreaId, FuelTypeId, PhotoEntityPath")).FirstOrDefault();
            });
        }

        public async Task<IEnumerable<Car>> GetAllCarsCollectionAsync()
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<Car, SalesArea, FuelTypeEntity, PhotoEntity, Car>(
                    GetAllCars.Value,
                    (car, area, fuel, photo) =>
                    {
                        car.SalesArea = area;
                        car.FuelType = fuel;
                        car.PhotoEntityPath = photo;
                        return car;
                    },
                    splitOn: "AreaId, FuelTypeId, PhotoEntityPath"));
            });
        }

        public async Task<long> CreateManagerAsync(Car car)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    CreateCar.Value,
                    new
                    {
                        car.Brand,
                        car.Model,
                        car.Year,
                        car.EngineVolume,
                        car.Mileage,
                        car.Description,
                        car.Price,
                        car.VinCode,
                        car.Color,
                        car.Body,
                        AreaId = car.SalesArea.Id,
                        FuelTypeId = car.FuelType.FuelType,
                        PhotoPath = car.PhotoEntityPath.PhotoPath
                    });

                await conn.ExecuteAsync(command);
            });

            return await GetLatestCarIdAsync(await GetConnection());
        }

        public async Task UpdateCarAsync(Car car)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    UpdateCar.Value,
                    new
                    {
                        car.Id,
                        car.Brand,
                        car.Model,
                        car.Year,
                        car.EngineVolume,
                        car.Mileage,
                        car.Description,
                        car.Price,
                        car.VinCode,
                        car.Color,
                        car.Body,
                        AreaId = car.SalesArea.Id,
                        FuelTypeId = car.FuelType.FuelType,
                        PhotoPath = car.PhotoEntityPath.PhotoPath
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
