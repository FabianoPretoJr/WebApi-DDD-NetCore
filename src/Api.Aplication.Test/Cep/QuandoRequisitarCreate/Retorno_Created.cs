using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Cep;
using Api.Domain.Interfaces.Services.CEP;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Cep.QuandoRequisitarCreate
{
    public class Retorno_Created
    {
        private CepsController _controller;

        [Fact(DisplayName = "É Possível Realizar o Created")]
        public async Task E_Possivel_Invocar_a_Controller_Created()
        {
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(m => m.Post(It.IsAny<CepCreateDTO>())).ReturnsAsync(
                new CepCreateResultDTO
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(10000, 99999).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 2000).ToString(),
                    MunicipioId = Guid.NewGuid(),
                    CreateAt = DateTime.Now
                }
            );

            _controller = new CepsController(serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var cepCreateDTO = new CepCreateDTO
            {
                Cep = Faker.RandomNumber.Next(10000, 99999).ToString(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(1, 2000).ToString(),
                MunicipioId = Guid.NewGuid()
            };

            var result = await _controller.Post(cepCreateDTO);
            Assert.True(result is CreatedResult);
        }
    }
}