using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Municipio.QuandoRequisitarCreate
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Create")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_Create()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.Post(It.IsAny<MunicipioCreateDTO>())).ReturnsAsync(
                new MunicipioCreateResultDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                    UfId = Guid.NewGuid(),
                    CreateAt = DateTime.Now
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Nome", "É um campo obrigatório");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var municipioCreateDTO = new MunicipioCreateDTO
            {
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                UfId = Guid.NewGuid()
            };

            var result = await _controller.Post(municipioCreateDTO);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}