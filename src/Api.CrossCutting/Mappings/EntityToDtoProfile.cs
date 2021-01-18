using Api.Domain.DTO.Cep;
using Api.Domain.DTO.Municipio;
using Api.Domain.DTO.Uf;
using Api.Domain.DTO.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDTO, UserEntity>()
                .ReverseMap();

            CreateMap<UserCreateResultDTO, UserEntity>()
                .ReverseMap();

            CreateMap<UserUpdateResultDTO, UserEntity>()
                .ReverseMap();

            CreateMap<UfDTO, UfEntity>()
                .ReverseMap();

            CreateMap<MunicipioDTO, MunicipioEntity>()
                .ReverseMap();
            
            CreateMap<MunicipioCompletoDTO, MunicipioEntity>()
                .ReverseMap();

            CreateMap<MunicipioCreateResultDTO, MunicipioEntity>()
                .ReverseMap();

            CreateMap<MunicipioUpdateResultDTO, MunicipioEntity>()
                .ReverseMap();

            CreateMap<CepDTO, CepEntity>()
                .ReverseMap();

            CreateMap<CepCreateResultDTO, CepEntity>()
                .ReverseMap();

            CreateMap<CepUpdateResultDTO, CepEntity>()
                .ReverseMap();
        }
    }
}