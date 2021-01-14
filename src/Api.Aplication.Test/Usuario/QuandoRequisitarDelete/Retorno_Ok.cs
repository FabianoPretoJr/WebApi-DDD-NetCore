using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Usuario.QuandoRequisitarDelete
{
    public class Retorno_Ok
    {
        private UsersController _controller;
        [Fact(DisplayName = "É Possível Realizar o Delete")]
        public async Task E_Possivel_Invocar_a_Controller_Delete()
        {
            var _serviceMock = new Mock<IUserService>();

            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UsersController(_serviceMock.Object);

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);
            Assert.True(_controller.ModelState.IsValid);
            
            var resultValue = ((OkObjectResult)result).Value;
            Assert.NotNull(resultValue);
            Assert.True((Boolean)resultValue);
        }
    }
}