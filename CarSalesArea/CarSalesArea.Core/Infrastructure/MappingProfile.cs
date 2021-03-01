using AutoMapper;
using CarSalesArea.Core.Models;
using CarSalesArea.Data.Models;
using PagingOptions = CarSalesArea.Core.Models.PagingOptions;

namespace CarSalesArea.Core.Infrastructure
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<SalesArea, SalesAreaModel>().ReverseMap();
            CreateMap<ManagerEntity, ManagerModel>().ReverseMap();
            CreateMap<CarEntity, CarModel>().ReverseMap();
            CreateMap<FuelTypeEntity, FuelTypeModel>().ReverseMap();
            CreateMap<PhotoEntity, PhotoModel>().ReverseMap();
            CreateMap<Data.Models.PagingOptions, PagingOptions>().ReverseMap();
        }
    }
}
