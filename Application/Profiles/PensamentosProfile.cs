using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Application.Profiles 
{
    public class PensamentosProfile : Profile
    {
        public PensamentosProfile() {


            CreateMap<PensamentosDto, Pensamentos>()
            .ForMember(dest => dest.Modelos, opt => opt.MapFrom(src => src.Modelos))
            .ReverseMap();

        }

    }
}
