using System;
using System.Collections.Generic;
using Api.Domain.DTO.Cep;
using Api.Domain.DTO.Municipio;
using Api.Domain.DTO.Uf;

namespace Api.Service.Test.Cep
{
    public class CepTestes
    {
        public static string Cep { get; set; }
        public static string CepAtualizado { get; set; }
        public static string Logradouro { get; set; }
        public static string LogradouroAtualizado { get; set; }
        public static string Numero { get; set; }
        public static string NumeroAtualizado { get; set; }
        public static Guid IdCep { get; set; }
        public static Guid MunicipioId { get; set; }

        public CepDTO cepDTO;
        public CepCreateDTO cepCreateDTO;
        public CepCreateResultDTO cepCreateResultDTO;
        public CepUpdateDTO cepUpdateDTO;
        public CepUpdateResultDTO cepUpdateResultDTO;

        public CepTestes()
        {
            IdCep = Guid.NewGuid();
            Cep = Faker.RandomNumber.Next(10000, 99999).ToString();
            CepAtualizado = Faker.RandomNumber.Next(10000, 99999).ToString();
            Logradouro = Faker.Address.StreetName();
            LogradouroAtualizado = Faker.Address.StreetName();
            Numero = Faker.RandomNumber.Next(1, 1000).ToString();
            NumeroAtualizado = Faker.RandomNumber.Next(1, 1000).ToString();
            MunicipioId = Guid.NewGuid();

            cepDTO = new CepDTO
            {
                Id = IdCep,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = MunicipioId,
                Municipio = new MunicipioCompletoDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid(),
                    Uf = new UfDTO
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)
                    }
                }
            };

            cepCreateDTO = new CepCreateDTO
            {
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = MunicipioId
            };

            cepCreateResultDTO = new CepCreateResultDTO
            {
                Id = IdCep,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = MunicipioId,
                CreateAt = DateTime.Now
            };

            cepUpdateDTO = new CepUpdateDTO
            {
                Id = IdCep,
                Cep = CepAtualizado,
                Logradouro = LogradouroAtualizado,
                Numero = NumeroAtualizado,
                MunicipioId = MunicipioId
            };

            cepUpdateResultDTO = new CepUpdateResultDTO
            {
                Id = IdCep,
                Cep = CepAtualizado,
                Logradouro = LogradouroAtualizado,
                Numero = NumeroAtualizado,
                MunicipioId = MunicipioId,
                UpdateAt = DateTime.Now
            };
        }
    }
}