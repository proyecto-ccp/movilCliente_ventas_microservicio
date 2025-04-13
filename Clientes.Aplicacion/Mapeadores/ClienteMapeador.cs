using AutoMapper;
using Clientes.Dominio.Entidades;
using Clientes.Aplicacion.Dto;
namespace Clientes.Aplicacion.Mapeadores
{
    public class ClienteMapeador : Profile
    {
        public ClienteMapeador()
        {
            CreateMap<Cliente,ClienteDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<Cliente,ClienteIn>()
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<ClienteOut,ClienteIn>()
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Cliente.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Cliente.Apellido))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Cliente.Documento))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Cliente.Email));
        }
    }
}
