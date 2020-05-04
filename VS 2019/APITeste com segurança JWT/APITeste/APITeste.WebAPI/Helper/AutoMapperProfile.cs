#region REFERÊNCIAS ADICIONADAS
using AutoMapper;
using APITeste.Dominio;
using APITeste.WebAPI.DTO.UsuarioDTO;
#endregion

namespace APITeste.WebAPI.Helper
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
