using AutoMapper;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services
{
    public class SalesAreaService: ISalesAreaService
    {
        private readonly ISalesAreaRepository _salesAreaRepository;
        private readonly IMapper _mapper;

        public SalesAreaService(ISalesAreaRepository salesAreaRepository, IMapper mapper)
        {
            _salesAreaRepository = salesAreaRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<SalesAreaModel> GetSalesAreaByIdAsync(long id)
        {
            var area = await _salesAreaRepository.GetSalesAreaByIdAsync(id);

            var result = _mapper.Map<SalesAreaModel>(area);

            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SalesAreaModel>> GetAllSalesAreasAsync()
        {
            var areaCollection = await _salesAreaRepository.GetAllSalesAreasCollectionAsync();
            var result = _mapper.Map<IEnumerable<SalesAreaModel>>(areaCollection);

            return result;
        }

        /// <inheritdoc/>
        public async Task<long> CreateSalesAreaAsync(SalesAreaModel salesAreaModel)
        {
            var salesArea = _mapper.Map<SalesArea>(salesAreaModel);
            var result = await _salesAreaRepository.CreateSalesAreaAsync(salesArea);

            return result;
        }

        /// <inheritdoc/>
        public async Task UpdateSalesAreaAsync(SalesAreaModel salesAreaModel)
        {
            var salesArea = _mapper.Map<SalesArea>(salesAreaModel);
            await _salesAreaRepository.UpdateSalesAreaAsync(salesArea);
        }

        /// <inheritdoc/>
        public async Task RemoveSalesAreaAsync(long id)
        {
            await _salesAreaRepository.DeleteSalesAreaAsync(id);
        }
    }
}
