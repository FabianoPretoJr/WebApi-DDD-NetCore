using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.CEP;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Cep.QuandoRequisitarDelete
{
    public class Retorno_BadRequest
    {
        private CepsController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Delete")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_Delete()
        {
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);

            _controller = new CepsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
        }
    }
}