using AutoMapper;
using CarSalesArea.Api.Controllers;
using CarSalesArea.Api.ViewModels;
using CarSalesArea.Core.Models;

namespace CarSalesArea.Api.Infrastructure
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ManagerModel, ManagerViewModel>()
                .ForMember(dest => dest.Self,
                    opt
                        => opt.MapFrom(src
                            => Link.To(
                                nameof(ManagerController.GetManagerByIdAsync),
                                new {id = src.Id})))
                .ReverseMap();

            CreateMap<SalesAreaModel, SalesAreaViewModel>()
                .ForMember(dest => dest.Self,
                    opt
                        => opt.MapFrom(src
                            => Link.To(
                                nameof(SalesAreaController.GetSalesAreaByIdAsync),
                                new {areaId = src.Id})))
                .ReverseMap();

            CreateMap<CarModel, CarViewModel>()
                .ForMember(dest => dest.Self,
                    opt => opt.MapFrom(src =>
                        Link.To(nameof(CarController.GetCarByIdAsync),
                            new {id = src.Id})))
                .ReverseMap();

            CreateMap<FuelTypeModel, FuelTypeViewModel>().ReverseMap();
            CreateMap<PhotoModel, PhotoViewModel>().ReverseMap();
        }
    }
}
