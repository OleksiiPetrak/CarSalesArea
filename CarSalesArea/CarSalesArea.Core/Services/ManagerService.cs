using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarSalesArea.Core.Models;

namespace CarSalesArea.Core.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public ManagerService(
            IManagerRepository managerRepository, 
            IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<long> CreateManagerAsync(ManagerModel manager)
        {
            var entity = _mapper.Map<Manager>(manager);
            var managerId = await _managerRepository.CreateManagerAsync(entity);
            return managerId;
        }

        public async Task<IEnumerable<ManagerModel>> GetAllManagersAsync()
        {
            var managers = await _managerRepository.GetAllManagersCollectionAsync();

            var result = _mapper.Map<IEnumerable<ManagerModel>>(managers);

            return result;
        }

        public async Task<ManagerModel> GetManagerByIdAsync(long id)
        {
            var manager = await _managerRepository.GetManagerByIdAsync(id);

            if (manager == null)
            {
                throw new NullReferenceException();
            }

            var result = _mapper.Map<ManagerModel>(manager);

            return result;
        }

        public async Task RemoveManagerAsync(long id)
        {
            await _managerRepository.DeleteManagerAsync(id);
        }

        public async Task UpdateManagerAsync(ManagerModel manager)
        {
            var entity = _mapper.Map<Manager>(manager);
            await _managerRepository.UpdateManagerAsync(entity);
        }
    }
}
