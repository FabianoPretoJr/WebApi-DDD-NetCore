using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Municipio.QuandoRequisitarGetAll
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o GetAll")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<MunicipioDTO>
                {
                    new MunicipioDTO
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                        UfId = Guid.NewGuid()
                    },
                    new MunicipioDTO
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                        UfId = Guid.NewGuid()
                    }
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }
}