using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Municipio.QuandoRequisitarGetCompleteByIBGE
{
    public class Retorno_NotFound
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Get Complete By IBGE")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_GetCompleteByIBGE()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.GetCompleteByIBGE(It.IsAny<int>())).Returns(Task.FromResult((MunicipioCompletoDTO)null));

            _controller = new MunicipiosController(serviceMock.Object);

            var result = await _controller.GetCompleteByIBGE(It.IsAny<int>());
            Assert.True(result is NotFoundResult);
        }
    }
}