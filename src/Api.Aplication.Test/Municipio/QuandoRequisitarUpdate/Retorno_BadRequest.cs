using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Municipio.QuandoRequisitarUpdate
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Update")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_Update()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.Put(It.IsAny<MunicipioUpdateDTO>())).ReturnsAsync(
                new MunicipioUpdateResultDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                    UfId = Guid.NewGuid(),
                    UpdateAt= DateTime.Now
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Nome", "É um campo obrigatório");

            var municipioUpdateDTO = new MunicipioUpdateDTO
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                UfId = Guid.NewGuid()
            };

            var result = await _controller.Put(municipioUpdateDTO);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}