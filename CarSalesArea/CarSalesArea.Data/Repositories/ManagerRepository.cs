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
        private static Lazy<string> GetAllManagers = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> GetManagerById = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> GetLatestManagerId = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> CreateManager = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> UpdateManager = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> DeleteManager = ScriptLoader.GetLazyEmbeddedResource<Manager>();

        public ManagerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Manager> GetManagerByIdAsync(long id)
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<Manager, SalesArea, Manager>(
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

        public async Task<IEnumerable<Manager>> GetAllManagersCollectionAsync()
        {
            return await WithConnection(async conn =>
            {
                return (await conn.QueryAsync<Manager, SalesArea, Manager>(
                    GetAllManagers.Value,
                    (manager, area) =>
                    {
                        manager.SalesArea = area;
                        return manager;
                    },
                    splitOn: "AreaId"));
            });
        }

        public async Task<long> CreateManagerAsync(Manager manager)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    CreateManager.Value,
                    new
                    {
                        manager.ManagerName,
                        manager.Surname,
                        AreaId = manager.SalesArea.Id
                    });

                await conn.ExecuteAsync(command);
            });

            return await GetLatestManagerIdAsync(await GetConnection());
        }

        public async Task UpdateManagerAsync(Manager manager)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    UpdateManager.Value,
                    new
                    {
                        manager.Id,
                        manager.ManagerName,
                        manager.Surname,
                        AreaId = manager.SalesArea.Id
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
