using APITeste2017.Dominio;
using APITeste2017.WebAPI.DTO.UsuarioDto;
using AutoMapper;

namespace APITeste2017.WebAPI.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
            CreateMap<Usuario, ReturnUsuarioLogin>().ReverseMap();
        }
    }
}
