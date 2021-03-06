using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Usuario.QuandoRequisitarCreate
{
    public class Retorno_Created
    {
        private UsersController _controller;
        [Fact(DisplayName = "É Possível Realizar o Created")]
        public async Task E_Possivel_Invocar_a_Controller_Create()
        {
            var _serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            _serviceMock.Setup(m => m.Post(It.IsAny<UserCreateDTO>())).ReturnsAsync(
                new UserCreateResultDTO
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    CreateAt = DateTime.Now
                }
            );

            _controller = new UsersController(_serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var userCreateDTO = new UserCreateDTO
            {
                Name = nome,
                Email = email
            };

            var result = await _controller.Post(userCreateDTO);
            Assert.True(result is CreatedResult);
            Assert.True(_controller.ModelState.IsValid);

            var resultValue = ((CreatedResult)result).Value as UserCreateResultDTO;
            Assert.NotNull(resultValue);
            Assert.Equal(userCreateDTO.Name, resultValue.Name);
            Assert.Equal(userCreateDTO.Email, resultValue.Email);
        }
    }
}