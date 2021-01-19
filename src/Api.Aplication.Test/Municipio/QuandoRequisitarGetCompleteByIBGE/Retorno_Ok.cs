using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Municipio;
using Api.Domain.DTO.Uf;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Municipio.QuandoRequisitarGetCompleteByIBGE
{
    public class Retorno_Ok
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É Possível Realizar o Get Comple By IBGE")]
        public async Task E_Possivel_Invocar_a_Controller_GetCompleteByIBGE()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.GetCompleteByIBGE(It.IsAny<int>())).ReturnsAsync(
                new MunicipioCompletoDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                    UfId = Guid.NewGuid(),
                    Uf = new UfDTO
                    {
                        Id = Guid.NewGuid(),
                        Nome = "São Paulo",
                        Sigla = "SP"
                    }
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);

            var result = await _controller.GetCompleteByIBGE(It.IsAny<int>());
            Assert.True(result is OkObjectResult);
        }
    }
}