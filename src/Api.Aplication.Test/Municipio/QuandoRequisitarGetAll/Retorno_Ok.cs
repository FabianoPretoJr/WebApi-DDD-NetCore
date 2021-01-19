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
    public class Retorno_Ok
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É Possível Realizar o Get All")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
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

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);
        }
    }
}