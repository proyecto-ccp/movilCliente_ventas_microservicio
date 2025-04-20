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
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<Cliente,ClienteIn>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.IdZona, opt => opt.MapFrom(src => src.IdZona))
                .ReverseMap();

            CreateMap<ClienteOut,ClienteIn>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Cliente.Apellido))
                .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.Cliente.TipoDocumento))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Cliente.Documento))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Cliente.Direccion))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Cliente.Telefono))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Cliente.Email))
                .ForMember(dest => dest.IdZona, opt => opt.MapFrom(src => src.Cliente.IdZona))
                .ReverseMap();
        }
    }
}
