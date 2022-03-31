using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CarSalesArea.Data.Helpers;
using Dapper;

namespace CarSalesArea.Data.Repositories
{
    public class SalesAreaRepository: BaseRepository, ISalesAreaRepository
    {
        private static Lazy<string> GetAllSalesAreas = ScriptLoader.GetLazyEmbeddedResource<SalesArea>();
        private static Lazy<string> GetSalesAreaById = ScriptLoader.GetLazyEmbeddedResource<SalesArea>();
        private static Lazy<string> GetLatestSalesAreaId = ScriptLoader.GetLazyEmbeddedResource<SalesArea>();
        private static Lazy<string> CreateSalesArea = ScriptLoader.GetLazyEmbeddedResource<SalesArea>();
        private static Lazy<string> UpdateSalesArea = ScriptLoader.GetLazyEmbeddedResource<SalesArea>();
        private static Lazy<string> DeleteSalesArea = ScriptLoader.GetLazyEmbeddedResource<SalesArea>();

        public SalesAreaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<SalesArea> GetSalesAreaByIdAsync(long id)
        {
            return await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    GetSalesAreaById.Value,
                    new {Id = id});

                return (await conn.QueryAsync<SalesArea>(command)).FirstOrDefault();
            });
        }

        public async Task<IEnumerable<SalesArea>> GetAllSalesAreasCollectionAsync()
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<SalesArea>(GetAllSalesAreas.Value);
                return query;
            });
        }

        public async Task<long> CreateSalesAreaAsync(SalesArea manager)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    CreateSalesArea.Value,
                    new
                    {
                        manager.AreaLocation,
                        manager.Capacity
                    });

                await conn.ExecuteAsync(command);
            });

            return await GetLatestSalesAreaIdAsync(await GetConnection());
        }

        public async Task UpdateSalesAreaAsync(SalesArea manager)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    UpdateSalesArea.Value,
                    new
                    {
                        manager.Id,
                        manager.AreaLocation,
                        manager.Capacity
                    });

                await conn.ExecuteAsync(command);
            });
        }

        public async Task DeleteSalesAreaAsync(long id)
        {
            await WithConnection(async conn =>
            {
                var command = new CommandDefinition(
                    DeleteSalesArea.Value,
                    new
                    {
                        Id = id
                    });

                await conn.ExecuteAsync(command);
            });
        }

        private async Task<long> GetLatestSalesAreaIdAsync(IDbConnection connection)
        {
            var command = new CommandDefinition(
                GetLatestSalesAreaId.Value);

            var result = await connection.QueryFirstOrDefaultAsync<long>(command);

            connection.Close();

            return result;
        }
    }
}
