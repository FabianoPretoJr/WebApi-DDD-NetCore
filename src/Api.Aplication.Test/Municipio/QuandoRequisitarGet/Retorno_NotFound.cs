using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Municipio.QuandoRequisitarGet
{
    public class Retorno_NotFound
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Get")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.GetId(It.IsAny<Guid>())).Returns(Task.FromResult((MunicipioDTO)null));

            _controller = new MunicipiosController(serviceMock.Object);

            var result = await _controller.GetId(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }
    }
}