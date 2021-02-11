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
    public class ManagerRepository: BaseRepository, IManagerRepository
    {
        private static Lazy<string> GetAllManagers = ScriptLoader.GetLazyEmbeddedResource<ManagerEntity>();
        private static Lazy<string> GetManagerById = ScriptLoader.GetLazyEmbeddedResource<ManagerEntity>();
        private static Lazy<string> GetLatestManagerId = ScriptLoader.GetLazyEmbeddedResource<ManagerEntity>();
        private static Lazy<string> CreateManager = ScriptLoader.GetLazyEmbeddedResource<ManagerEntity>();
        private static Lazy<string> UpdateManager = ScriptLoader.GetLazyEmbeddedResource<ManagerEntity>();
        private static Lazy<string> DeleteManager = ScriptLoader.GetLazyEmbeddedResource<ManagerEntity>();

        public ManagerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<ManagerEntity> GetManagerByIdAsync(long id)
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<ManagerEntity, SalesArea, ManagerEntity>(
                    GetManagerById.Value,
                    (manager, area) =>
                    {
                        manager.SalesArea = area;
                        return manager;
                    },
                    new
                    {
                        Id = id
                    },
                    splitOn: "AreaId")).FirstOrDefault();
            });
        }

        public async Task<IEnumerable<ManagerEntity>> GetAllManagersCollectionAsync()
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<ManagerEntity, SalesArea, ManagerEntity>(
                    GetAllManagers.Value,
                    (manager, area) =>
                    {
                        manager.SalesArea = area;
                        return manager;
                    },
                    splitOn: "AreaId"));
            });
        }

        public async Task<long> CreateManagerAsync(ManagerEntity managerEntity)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    CreateManager.Value,
                    new
                    {
                        managerEntity.ManagerName,
                        managerEntity.Surname,
                        AreaId = managerEntity.SalesArea.Id
                    });

                await conn.ExecuteAsync(command);
            });

            return await GetLatestManagerIdAsync(await GetConnection());
        }

        public async Task UpdateManagerAsync(ManagerEntity managerEntity)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    UpdateManager.Value,
                    new
                    {
                        managerEntity.Id,
                        managerEntity.ManagerName,
                        managerEntity.Surname,
                        AreaId = managerEntity.SalesArea.Id
                    });

                await conn.ExecuteAsync(command);
            });
        }

        public async Task DeleteManagerAsync(long id)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    DeleteManager.Value,
                    new
                    {
                        Id = id
                    });

                await conn.ExecuteAsync(command);
            });
        }

        private async Task<long> GetLatestManagerIdAsync(IDbConnection connection)
        {
            var command = new CommandDefinition(
                GetLatestManagerId.Value);

            var result = await connection.QueryFirstOrDefaultAsync<long>(command);

            connection.Close();

            return result;
        }
    }
}
