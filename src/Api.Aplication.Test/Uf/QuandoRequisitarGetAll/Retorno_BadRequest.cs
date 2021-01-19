using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTO.Uf;
using Api.Domain.Interfaces.Services.UF;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Aplication.Test.Uf.QuandoRequisitarGetAll
{
    public class Retorno_BadRequest
    {
        private UfsController _controller;

        [Fact(DisplayName = "Não É Possível Realizar o Get All")]
        public async Task Nao_E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IUfService>();

            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<UfDTO>
                {
                    new UfDTO
                    {
                        Id = Guid.NewGuid(),
                        Nome = "São Paulo",
                        Sigla = "SP"
                    },
                    new UfDTO
                    {
                        Id = Guid.NewGuid(),
                        Nome = "Amazonas",
                        Sigla = "AM"
                    }
                }
            );

            _controller = new UfsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }
}