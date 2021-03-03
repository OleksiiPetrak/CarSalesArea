using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CarSalesArea.Data.Helpers;

namespace CarSalesArea.Data.Repositories
{
    public class MediaRepository: BaseRepository, IMediaRepository
    {
        private static Lazy<string> GetAllPhotos = ScriptLoader.GetLazyEmbeddedResource<PhotoEntity>();
        private static Lazy<string> GetPhotosByCarId = ScriptLoader.GetLazyEmbeddedResource<PhotoEntity>();
        private static Lazy<string> GetLatestPhotoId = ScriptLoader.GetLazyEmbeddedResource<PhotoEntity>();
        private static Lazy<string> CreatePhoto = ScriptLoader.GetLazyEmbeddedResource<PhotoEntity>();
        private static Lazy<string> UpdatePhoto = ScriptLoader.GetLazyEmbeddedResource<PhotoEntity>();
        private static Lazy<string> DeletePhoto = ScriptLoader.GetLazyEmbeddedResource<PhotoEntity>();

        public MediaRepository(IConfiguration configuration): base(configuration)
        {
        }

        public async Task<IEnumerable<PhotoEntity>> GetPhotoCollectionByCarIdAsync(long carId)
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<PhotoEntity, CarEntity, PhotoEntity>(
                    GetPhotosByCarId.Value,
                    (photo, car) =>
                    {
                        photo.Car = car;
                        return photo;
                    },
                    new
                    {
                        Id = carId
                    },
                    splitOn: "CarId"
                ));
            });
        }

        public async Task<IEnumerable<PhotoEntity>> GetAllPhotosCollectionAsync()
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<PhotoEntity, CarEntity, PhotoEntity>(
                    GetAllPhotos.Value,
                    (photo, car) =>
                    {
                        photo.Car = car;
                        return photo;
                    },
                    splitOn: "CarId"
                ));
            });
        }

        public async Task<long> CreatePhotoAsync(PhotoEntity photoEntity)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    CreatePhoto.Value,
                    new
                    {
                        photoEntity.PhotoPath,
                        CarId = photoEntity.Car.Id
                    });

                await conn.ExecuteAsync(command);
            });

            return await GetLatestPhotoIdAsync(await GetConnection());
        }

        public async Task UpdatePhotoAsync(PhotoEntity photoEntity)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    UpdatePhoto.Value,
                    new
                    {
                        photoEntity.Id,
                        photoEntity.PhotoPath,
                        CarId = photoEntity.Car.Id
                    });

                await conn.ExecuteAsync(command);
            });
        }

        public async Task DeletePhotoAsync(long id)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    DeletePhoto.Value,
                    new
                    {
                        Id = id
                    });

                await conn.ExecuteAsync(command);
            });
        }

        private async Task<long> GetLatestPhotoIdAsync(IDbConnection connection)
        {
            var command = new CommandDefinition(
                GetLatestPhotoId.Value);

            var result = await connection.QueryFirstOrDefaultAsync<long>(command);

            connection.Close();

            return result;
        }
    }
}
