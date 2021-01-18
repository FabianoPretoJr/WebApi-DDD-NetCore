using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTO.Municipio;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class MunicipioMapper : BaseTesteService
    {
        [Fact(DisplayName = "É Possível Mapear os Modelo de Municipio")]
        public void E_Possivel_Mapear_os_Modelos_Municipios()
        {
            var model = new MunicipioModel()
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = Guid.NewGuid(),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            var listaEntity = new List<MunicipioEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new MunicipioEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Uf = new UfEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1 ,3)
                    }
                };
                listaEntity.Add(item);
            }

            // Model => Entity
            var entity = Mapper.Map<MunicipioEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.CodIBGE, model.CodIBGE);
            Assert.Equal(entity.UfId, model.UfId);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            // Entity => DTO
            var municipioDTO = Mapper.Map<MunicipioDTO>(entity);
            Assert.Equal(municipioDTO.Id, entity.Id);
            Assert.Equal(municipioDTO.Nome, entity.Nome);
            Assert.Equal(municipioDTO.CodIBGE, entity.CodIBGE);
            Assert.Equal(municipioDTO.UfId, entity.UfId);

            var municipioCompletoDTO = Mapper.Map<MunicipioCompletoDTO>(listaEntity.FirstOrDefault());
            Assert.Equal(municipioCompletoDTO.Id, listaEntity.FirstOrDefault().Id);
            Assert.Equal(municipioCompletoDTO.Nome, listaEntity.FirstOrDefault().Nome);
            Assert.Equal(municipioCompletoDTO.CodIBGE, listaEntity.FirstOrDefault().CodIBGE);
            Assert.Equal(municipioCompletoDTO.UfId, listaEntity.FirstOrDefault().UfId);
            Assert.NotNull(municipioCompletoDTO.Uf);

            var listaDTO = Mapper.Map<List<MunicipioDTO>>(listaEntity);
            Assert.True(listaDTO.Count() == listaEntity.Count());
            for (int i = 0; i < listaDTO.Count(); i++)
            {
                Assert.Equal(listaDTO[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDTO[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listaDTO[i].CodIBGE, listaEntity[i].CodIBGE);
                Assert.Equal(listaDTO[i].UfId, listaEntity[i].UfId);
            }

            var municipioCreateResultDTO = Mapper.Map<MunicipioCreateResultDTO>(entity);
            Assert.Equal(municipioCreateResultDTO.Id, entity.Id);
            Assert.Equal(municipioCreateResultDTO.Nome, entity.Nome);
            Assert.Equal(municipioCreateResultDTO.CodIBGE, entity.CodIBGE);
            Assert.Equal(municipioCreateResultDTO.UfId, entity.UfId);
            Assert.Equal(municipioCreateResultDTO.CreateAt, entity.CreateAt);

            var municipioUpdateResultDTO = Mapper.Map<MunicipioUpdateResultDTO>(entity);
            Assert.Equal(municipioUpdateResultDTO.Id, entity.Id);
            Assert.Equal(municipioUpdateResultDTO.Nome, entity.Nome);
            Assert.Equal(municipioUpdateResultDTO.CodIBGE, entity.CodIBGE);
            Assert.Equal(municipioUpdateResultDTO.UfId, entity.UfId);
            Assert.Equal(municipioUpdateResultDTO.UpdateAt, entity.UpdateAt);

            // DTO => Model
            var municipioModel = Mapper.Map<MunicipioModel>(municipioDTO);
            Assert.Equal(municipioModel.Id, municipioDTO.Id);
            Assert.Equal(municipioModel.Nome, municipioDTO.Nome);
            Assert.Equal(municipioModel.CodIBGE, municipioDTO.CodIBGE);
            Assert.Equal(municipioModel.UfId, municipioDTO.UfId);

            var municipioCreateDTO = Mapper.Map<MunicipioCreateDTO>(municipioModel);
            Assert.Equal(municipioCreateDTO.Nome, municipioModel.Nome);
            Assert.Equal(municipioCreateDTO.CodIBGE, municipioModel.CodIBGE);
            Assert.Equal(municipioCreateDTO.UfId, municipioModel.UfId);

            var municipioUpdateDTO = Mapper.Map<MunicipioUpdateDTO>(municipioModel);
            Assert.Equal(municipioUpdateDTO.Id, municipioModel.Id);
            Assert.Equal(municipioUpdateDTO.Nome, municipioModel.Nome);
            Assert.Equal(municipioUpdateDTO.CodIBGE, municipioModel.CodIBGE);
            Assert.Equal(municipioUpdateDTO.UfId, municipioModel.UfId);
        }
    }
}