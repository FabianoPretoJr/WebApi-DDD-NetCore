using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Cep;
using Api.Domain.Interfaces.Services.CEP;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Cep.QuandoRequisitarUpdate
{
    public class Retorno_BadRequest
    {
        private CepsController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Update")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_Update()
        {
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(m => m.Put(It.IsAny<CepUpdateDTO>())).ReturnsAsync(
                new CepUpdateResultDTO
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(10000, 99999).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 2000).ToString(),
                    MunicipioId = Guid.NewGuid(),
                    UpdateAt = DateTime.Now
                }
            );

            _controller = new CepsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Cep", "Cep é campo obrigatório");

            var cepUpdateDTO = new CepUpdateDTO
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(10000, 99999).ToString(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(1, 2000).ToString(),
                MunicipioId = Guid.NewGuid()
            };

            var result = await _controller.Put(cepUpdateDTO);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}