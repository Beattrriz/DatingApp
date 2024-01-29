using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MembroDto>() // onde queremos ir
            .ForMember(dest => dest.FotoUrl, opt => opt.MapFrom(src => src.Fotos.FirstOrDefault(x => x.Principal).Url))
            .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.DataNascimento.CalcularIdade()));
            CreateMap<Foto, FotoDto>(); //onde queremos mapear
            CreateMap<MembroUpdateDto, AppUser>(); //mandar as alterações para o banco
        }
    }
}