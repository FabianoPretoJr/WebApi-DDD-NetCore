using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Cep;
using Api.Domain.Interfaces.Services.CEP;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Cep.QuandoRequisitarGet
{
    public class Retorno_NotFound
    {
        private CepsController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Get Id")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_GetId()
        {
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((CepDTO)null));

            _controller = new CepsController(serviceMock.Object);

            var result = await _controller.GetId(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }
    }
}