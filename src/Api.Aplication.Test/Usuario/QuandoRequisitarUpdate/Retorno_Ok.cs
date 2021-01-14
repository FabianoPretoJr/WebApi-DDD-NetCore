using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Usuario.QuandoRequisitarUpdate
{
    public class Retorno_Ok
    {
        private UsersController _controller;
        [Fact(DisplayName = "É Possível Realizar o Update")]
        public async Task E_Possivel_Invocar_a_Controller_Update()
        {
            var _serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            _serviceMock.Setup(m => m.Put(It.IsAny<UserUpdateDTO>())).ReturnsAsync(
                new UserUpdateResultDTO
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    updateAt = DateTime.Now
                }
            );

            _controller = new UsersController(_serviceMock.Object);

            var userUpdateDTO = new UserUpdateDTO
            {
                Id = Guid.NewGuid(),
                Name = nome,
                Email = email,
            };

            var result = await _controller.Put(userUpdateDTO);
            Assert.True(result is OkObjectResult);
            Assert.True(_controller.ModelState.IsValid);

            var resultValue = ((OkObjectResult)result).Value as UserUpdateResultDTO;
            Assert.NotNull(resultValue);
            Assert.Equal(userUpdateDTO.Name, resultValue.Name);
            Assert.Equal(userUpdateDTO.Email, resultValue.Email);
        }
    }
}