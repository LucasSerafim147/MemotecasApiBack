using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Application.Profiles 
{
    public class PensamentosProfile : Profile
    {
        public PensamentosProfile() {
            CreateMap<PensamentosDto, Pensamentos>();

        }

    }
}
