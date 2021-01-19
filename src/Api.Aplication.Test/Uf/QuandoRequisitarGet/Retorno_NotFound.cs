using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Uf;
using Api.Domain.Interfaces.Services.UF;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Uf.QuandoRequisitarGet
{
    public class Retorno_NotFound
    {
        private UfsController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Get")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IUfService>();

            serviceMock.Setup(m => m.GetId(It.IsAny<Guid>())).Returns(Task.FromResult((UfDTO)null));

            _controller = new UfsController(serviceMock.Object);

            var result = await _controller.GetId(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }
    }
}