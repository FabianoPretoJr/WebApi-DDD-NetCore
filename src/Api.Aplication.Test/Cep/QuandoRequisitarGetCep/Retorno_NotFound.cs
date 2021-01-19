using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Cep;
using Api.Domain.Interfaces.Services.CEP;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Cep.QuandoRequisitarGetCep
{
    public class Retorno_NotFound
    {
        private CepsController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Get Cep")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_GetCep()
        {
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(m => m.Get(It.IsAny<string>())).Returns(Task.FromResult((CepDTO)null));

            _controller = new CepsController(serviceMock.Object);

            var result = await _controller.GetCep(It.IsAny<string>());
            Assert.True(result is NotFoundResult);
        }
    }
}