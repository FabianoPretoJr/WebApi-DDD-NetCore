using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Cep;
using Api.Domain.DTO.Municipio;
using Api.Domain.DTO.Uf;
using Api.Domain.Interfaces.Services.CEP;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Cep.QuandoRequisitarGetCep
{
    public class Retorno_Ok
    {
        private CepsController _controller;

        [Fact(DisplayName = "É Possível Realizar o Get Cep")]
        public async Task E_Possivel_Invocar_a_Controller_GetCep()
        {
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(m => m.Get(It.IsAny<string>())).ReturnsAsync(
                new CepDTO
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(10000, 99999).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 2000).ToString(),
                    MunicipioId = Guid.NewGuid(),
                    Municipio = new MunicipioCompletoDTO
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.City(),
                        CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                        UfId = Guid.NewGuid(),
                        Uf = new UfDTO
                        {
                            Id = Guid.NewGuid(),
                            Sigla = Faker.Address.UsState().Substring(1,2),
                            Nome = Faker.Address.UsState()
                        }
                    }
                }
            );

            _controller = new CepsController(serviceMock.Object);

            var result = await _controller.GetCep(It.IsAny<string>());
            Assert.True(result is OkObjectResult);
        }
    }
}