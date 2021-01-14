using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTO.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTesteService
    {
        [Fact(DisplayName = "É Possível Mapear os Modelos")]
        public void E_Possivel_Mapear_os_Modelos()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            var listaEntity = new List<UserEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                listaEntity.Add(item);
            }

            // Model => Entity
            var entity = Mapper.Map<UserEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Name, model.Name);
            Assert.Equal(entity.Email, model.Email);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            // Entity => DTO
            var userDTO = Mapper.Map<UserDTO>(entity);
            Assert.Equal(userDTO.Id, entity.Id);
            Assert.Equal(userDTO.Name, entity.Name);
            Assert.Equal(userDTO.Email, entity.Email);
            Assert.Equal(userDTO.CreateAt, entity.CreateAt);

            var listaDTO = Mapper.Map<List<UserDTO>>(listaEntity);
            Assert.True(listaDTO.Count() == listaEntity.Count());
            for(int i = 0; i < listaDTO.Count(); i++)
            {
                Assert.Equal(listaDTO[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDTO[i].Name, listaEntity[i].Name);
                Assert.Equal(listaDTO[i].Email, listaEntity[i].Email);
                Assert.Equal(listaDTO[i].CreateAt, listaEntity[i].CreateAt);
            }

            var userCreateResultDTO = Mapper.Map<UserCreateResultDTO>(entity);
            Assert.Equal(userCreateResultDTO.Id, entity.Id);
            Assert.Equal(userCreateResultDTO.Name, entity.Name);
            Assert.Equal(userCreateResultDTO.Email, entity.Email);
            Assert.Equal(userCreateResultDTO.CreateAt, entity.CreateAt);

            var userUpdateResultDTO = Mapper.Map<UserUpdateResultDTO>(entity);
            Assert.Equal(userUpdateResultDTO.Id, entity.Id);
            Assert.Equal(userUpdateResultDTO.Name, entity.Name);
            Assert.Equal(userUpdateResultDTO.Email, entity.Email);
            Assert.Equal(userUpdateResultDTO.updateAt, entity.UpdateAt);

            // DTO => Model
            var userModel = Mapper.Map<UserModel>(userDTO);
            Assert.Equal(userModel.Id, userDTO.Id);
            Assert.Equal(userModel.Name, userDTO.Name);
            Assert.Equal(userModel.Email, userDTO.Email);
            Assert.Equal(userModel.CreateAt, userDTO.CreateAt);

            var userCreateDTO = Mapper.Map<UserCreateDTO>(userModel);
            Assert.Equal(userCreateDTO.Name, userModel.Name);
            Assert.Equal(userCreateDTO.Email, userModel.Email);

            var userUpdateDTO = Mapper.Map<UserUpdateDTO>(userModel);
            Assert.Equal(userUpdateDTO.Id, userModel.Id);
            Assert.Equal(userUpdateDTO.Name, userModel.Name);
            Assert.Equal(userUpdateDTO.Email, userModel.Email);
        }
    }
}