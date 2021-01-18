using Api.Domain.DTO.Cep;
using Api.Domain.DTO.Municipio;
using Api.Domain.DTO.Uf;
using Api.Domain.DTO.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDTO>()
                .ReverseMap();

            CreateMap<UserModel, UserCreateDTO>()
                .ReverseMap();

            CreateMap<UserModel, UserUpdateDTO>()
                .ReverseMap();

            CreateMap<UfModel, UfDTO>()
                .ReverseMap();

            CreateMap<MunicipioModel, MunicipioDTO>()
                .ReverseMap();

            CreateMap<MunicipioModel, MunicipioCreateDTO>()
                .ReverseMap();

            CreateMap<MunicipioModel, MunicipioUpdateDTO>()
                .ReverseMap();

            CreateMap<CepModel, CepDTO>()
                .ReverseMap();

            CreateMap<CepModel, CepCreateDTO>()
                .ReverseMap();

            CreateMap<CepModel, CepUpdateDTO>()
                .ReverseMap();
        }
    }
}