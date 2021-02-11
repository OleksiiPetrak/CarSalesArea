using AutoMapper;
using CarSalesArea.Core.Models;
using CarSalesArea.Data.Models;

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
        }
    }
}
