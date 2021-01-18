using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTO.Cep;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class CepMapper : BaseTesteService
    {
        [Fact(DisplayName = "É Possível Mapear os Modelos de CEP")]
        public void E_Possivel_Mapear_os_Modelos_CEP()
        {
            var model = new CepModel
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1,10000).ToString(),
                Logradouro = Faker.Address.StreetName(),
                Numero = "",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                MunicipioId = Guid.NewGuid()
            };

            var listaEntity = new List<CepEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new CepEntity
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(1,10000).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1,10000).ToString(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Municipio = new MunicipioEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        CodIBGE = Faker.RandomNumber.Next(1,10000),
                        UfId = Guid.NewGuid(),
                        Uf = new UfEntity
                        {
                            Id = Guid.NewGuid(),
                            Nome = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3)
                        }
                    }
                };
                listaEntity.Add(item);
            }

            // Model => Entity
            var entity = Mapper.Map<CepEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Logradouro, model.Logradouro);
            Assert.Equal(entity.Numero, model.Numero);
            Assert.Equal(entity.Cep, model.Cep);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            // Entity => DTO
            var cepDTO = Mapper.Map<CepDTO>(entity);
            Assert.Equal(cepDTO.Id, entity.Id);
            Assert.Equal(cepDTO.Logradouro, entity.Logradouro);
            Assert.Equal(cepDTO.Numero, entity.Numero);
            Assert.Equal(cepDTO.Cep, entity.Cep);

            var cepCompletoDTO = Mapper.Map<CepDTO>(listaEntity.FirstOrDefault());
            Assert.Equal(cepCompletoDTO.Id, listaEntity.FirstOrDefault().Id);
            Assert.Equal(cepCompletoDTO.Logradouro, listaEntity.FirstOrDefault().Logradouro);
            Assert.Equal(cepCompletoDTO.Numero, listaEntity.FirstOrDefault().Numero);
            Assert.Equal(cepCompletoDTO.Cep, listaEntity.FirstOrDefault().Cep);
            Assert.NotNull(cepCompletoDTO.Municipio);
            Assert.NotNull(cepCompletoDTO.Municipio.Uf);

            var listaDTO = Mapper.Map<List<CepDTO>>(listaEntity);
            Assert.True(listaDTO.Count() == listaEntity.Count());
            for (int i = 0; i < listaDTO.Count(); i++)
            {
                Assert.Equal(listaDTO[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDTO[i].Logradouro, listaEntity[i].Logradouro);
                Assert.Equal(listaDTO[i].Numero, listaEntity[i].Numero);
                Assert.Equal(listaDTO[i].Cep, listaEntity[i].Cep);
            }

            var cepCreateResultDTO = Mapper.Map<CepCreateResultDTO>(entity);
            Assert.Equal(cepCreateResultDTO.Id, entity.Id);
            Assert.Equal(cepCreateResultDTO.Logradouro, entity.Logradouro);
            Assert.Equal(cepCreateResultDTO.Numero, entity.Numero);
            Assert.Equal(cepCreateResultDTO.Cep, entity.Cep);
            Assert.Equal(cepCreateResultDTO.CreateAt, entity.CreateAt);

            var cepUpdateResultDTO = Mapper.Map<CepUpdateResultDTO>(entity);
            Assert.Equal(cepUpdateResultDTO.Id, entity.Id);
            Assert.Equal(cepUpdateResultDTO.Logradouro, entity.Logradouro);
            Assert.Equal(cepUpdateResultDTO.Numero, entity.Numero);
            Assert.Equal(cepUpdateResultDTO.Cep, entity.Cep);
            Assert.Equal(cepUpdateResultDTO.UpdateAt, entity.UpdateAt);

            // DTO => Model
            cepDTO.Numero = "";
            var cepModel = Mapper.Map<CepModel>(cepDTO);
            Assert.Equal(cepModel.Id, cepDTO.Id);
            Assert.Equal(cepModel.Logradouro, cepDTO.Logradouro);
            Assert.Equal("S/N", cepModel.Numero);
            Assert.Equal(cepModel.Cep, cepDTO.Cep);

            var cepCreateDTO = Mapper.Map<CepCreateDTO>(cepModel);
            Assert.Equal(cepCreateDTO.Logradouro, cepModel.Logradouro);
            Assert.Equal(cepCreateDTO.Numero, cepModel.Numero);
            Assert.Equal(cepCreateDTO.Cep, cepModel.Cep);

            var cepUpdateDTO = Mapper.Map<CepUpdateDTO>(cepModel);
            Assert.Equal(cepUpdateDTO.Id, cepModel.Id);
            Assert.Equal(cepUpdateDTO.Logradouro, cepModel.Logradouro);
            Assert.Equal(cepUpdateDTO.Numero, cepModel.Numero);
            Assert.Equal(cepUpdateDTO.Cep, cepModel.Cep);
        }
    }
}