using AutoMapper;
using MapperApp.DTOs.Incoming;
using MapperApp.DTOs.Outgoing;
using MapperApp.Models;

namespace MapperApp.Profiles
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<DriverForCreationDto, Driver>()
                .ForMember( //Estamos dizendo aqui que o Id será automático
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()))

                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => $"{src.DriverNumber} - {src.FirstName.ToUpper()}" )) 
                                                //Adicionando o DriveNumber na frete do Nome
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(
                    dest => dest.DriverNumber,
                    opt => opt.MapFrom(src => src.DriverNumber))
                .ForMember(
                    dest => dest.WorldChampionships,
                    opt => opt.MapFrom(src => src.WorldChampionships))
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(
                    dest => dest.DateAdded,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(
                    dest => dest.DateUpdated,
                    opt => opt.MapFrom(src => DateTime.Now));




            CreateMap<Driver, DriverDto>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"))
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(x => x.Id))
                .ForMember(
                    dest => dest.DriverNumber,
                    opt => opt.MapFrom(x => x.DriverNumber))
                .ForMember(
                    dest => dest.WorldChampionships,
                    opt => opt.MapFrom(x => x.WorldChampionships));
        }
    }
}
