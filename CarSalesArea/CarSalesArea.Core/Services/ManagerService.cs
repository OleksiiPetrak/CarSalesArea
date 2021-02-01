using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarSalesArea.Data.Repositories.Interfaces;

namespace CarSalesArea.Core.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public Task CreateManagerAsync(Manager manager)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Manager>> GetAllManagersAsync()
        {
            var managers = _managerRepository.GetAllManagersCollectionAsync();

            return managers;
        }

        public Task<Manager> GetManagerByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveManagerAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateManagerAsync(Manager manager)
        {
            throw new NotImplementedException();
        }
    }
}
