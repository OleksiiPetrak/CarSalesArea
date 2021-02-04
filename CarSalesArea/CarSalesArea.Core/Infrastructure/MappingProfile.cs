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
            CreateMap<Manager, ManagerModel>().ReverseMap();
        }
    }
}
