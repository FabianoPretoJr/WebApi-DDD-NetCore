using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTO.Uf;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UfMapper : BaseTesteService
    {
        [Fact(DisplayName = "É Possível Mapear os Modelos de Uf")]
        public void E_Possivel_Mapear_os_Modelos_Uf()
        {
            var model = new UfModel
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.UsState(),
                Sigla = Faker.Address.UsState().Substring(1, 3),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            var listaEntity = new List<UfEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new UfEntity()
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                listaEntity.Add(item);
            }

            // Model => Entity
            var entity = Mapper.Map<UfEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.Sigla, model.Sigla);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            // Entity => DTO
            var ufDTO = Mapper.Map<UfDTO>(entity);
            Assert.Equal(ufDTO.Id, entity.Id);
            Assert.Equal(ufDTO.Nome, entity.Nome);
            Assert.Equal(ufDTO.Sigla, entity.Sigla);

            var listaDTO = Mapper.Map<List<UfDTO>>(listaEntity);
            Assert.True(listaDTO.Count() == listaEntity.Count());
            for (int i = 0; i < listaDTO.Count(); i++)
            {
                Assert.Equal(listaDTO[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDTO[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listaDTO[i].Sigla, listaEntity[i].Sigla);
            }

            // DTO para Model
            var ufModel = Mapper.Map<UfDTO>(model);
            Assert.Equal(ufModel.Id, model.Id);
            Assert.Equal(ufModel.Nome, model.Nome);
            Assert.Equal(ufModel.Sigla, model.Sigla);
        }
    }
}