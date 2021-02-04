using CarSalesArea.Data.Helpers;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CarSalesArea.Data.Repositories
{
    public class ManagerRepository: IManagerRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        private static Lazy<string> GetAllManagers = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> GetManagerById = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> GetLatestManagerId = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> CreateManager = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> UpdateManager = ScriptLoader.GetLazyEmbeddedResource<Manager>();
        private static Lazy<string> DeleteManager = ScriptLoader.GetLazyEmbeddedResource<Manager>();

        public ManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("CarSalesAreaConnection");
        }

        public async Task<Manager> GetManagerByIdAsync(long id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var command = new CommandDefinition(
                        GetManagerById.Value,
                        new{});

                    return (await connection.QueryAsync<Manager, SalesArea, Manager>(
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
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Manager>> GetAllManagersCollectionAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var managers 
                        = await connection.QueryAsync<Manager, SalesArea, Manager>(
                            GetAllManagers.Value,
                            (manager, area) =>
                            {
                                manager.SalesArea = area;
                                return manager;
                            },
                            splitOn:"AreaId");

                    return managers;
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<long> CreateManagerAsync(Manager manager)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var command = new CommandDefinition(
                        CreateManager.Value,
                        new
                        {
                            manager.ManagerName,
                            manager.Surname,
                            AreaId = manager.SalesArea.Id
                        });

                    await connection.ExecuteAsync(command);

                    return await GetLatestManagerIdAsync(connection);
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateManagerAsync(Manager manager)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var command = new CommandDefinition(
                        UpdateManager.Value,
                        new
                        {
                            manager.Id,
                            manager.ManagerName,
                            manager.Surname,
                            AreaId = manager.SalesArea.Id
                        });

                    await connection.ExecuteAsync(command);
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteManagerAsync(long id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var command = new CommandDefinition(
                        DeleteManager.Value,
                        new
                        {
                            Id = id
                        });

                    await connection.ExecuteAsync(command);
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task<long> GetLatestManagerIdAsync(SqlConnection connection)
        {
            var command = new CommandDefinition(
                GetLatestManagerId.Value);

            return await connection.QueryFirstOrDefaultAsync<long>(command);
        }
    }
}
