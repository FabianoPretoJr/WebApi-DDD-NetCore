using System;
using System.Collections.Generic;
using Api.Domain.DTO.Municipio;
using Api.Domain.DTO.Uf;

namespace Api.Service.Test.Municipio
{
    public class MunicipioTestes
    {
        public static string NomeMunicipio { get; set; }
        public static int CodigoIBGEMunicipio { get; set; }
        public static string NomeMunicipioAlterado { get; set; }
        public static int CodigoIBGEMunicipioAlterado { get; set; }
        public static Guid IdMunicipio { get; set; }
        public static Guid UfId { get; set; }

        public List<MunicipioDTO> listaDTO = new List<MunicipioDTO>();
        public MunicipioDTO municipioDTO;
        public MunicipioCompletoDTO municipioCompletoDTO;
        public MunicipioCreateDTO municipioCreateDTO;
        public MunicipioCreateResultDTO municipioCreateResultDTO;
        public MunicipioUpdateDTO municipioUpdateDTO;
        public MunicipioUpdateResultDTO municipioUpdateResultDTO;

        public MunicipioTestes()
        {
            IdMunicipio = Guid.NewGuid();
            NomeMunicipio = Faker.Address.City();
            CodigoIBGEMunicipio = Faker.RandomNumber.Next(1, 10000);
            NomeMunicipioAlterado = Faker.Address.City();
            CodigoIBGEMunicipioAlterado = Faker.RandomNumber.Next(1, 10000);
            UfId = Guid.NewGuid();

            for (int i = 0; i < 10; i++)
            {
                var item = new MunicipioDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid()
                };
                listaDTO.Add(item);
            }

            municipioDTO = new MunicipioDTO
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = UfId
            };

            municipioCompletoDTO = new MunicipioCompletoDTO
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = UfId,
                Uf = new UfDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3)
                }
            };

            municipioCreateDTO = new MunicipioCreateDTO
            {
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = UfId
            };

            municipioCreateResultDTO = new MunicipioCreateResultDTO
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = UfId,
                CreateAt = DateTime.Now
            };

            municipioUpdateDTO = new MunicipioUpdateDTO
            {
                Id = IdMunicipio,
                Nome = NomeMunicipioAlterado,
                CodIBGE = CodigoIBGEMunicipioAlterado,
                UfId = UfId
            };

            municipioUpdateResultDTO = new MunicipioUpdateResultDTO
            {
                Id = IdMunicipio,
                Nome = NomeMunicipioAlterado,
                CodIBGE = CodigoIBGEMunicipioAlterado,
                UfId = UfId,
                UpdateAt = DateTime.Now
            };
        }
    }
}