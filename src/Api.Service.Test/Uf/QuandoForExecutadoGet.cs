using System;
using System.Threading.Tasks;
using Api.Domain.DTO.Uf;
using Api.Domain.Interfaces.Services.UF;
using Moq;
using Xunit;

namespace Api.Service.Test.Uf
{
    public class QuandoForExecutadoGet : UfTestes
    {
        private IUfService _service;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método GET")]
        public async Task E_Possivel_Executar_Metodo_Get()
        {
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.GetId(UfId)).ReturnsAsync(ufDTO);
            _service = _serviceMock.Object;

            var result = await _service.GetId(UfId);
            Assert.NotNull(result);
            Assert.True(result.Id == UfId);
            Assert.Equal(Nome, result.Nome);

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.GetId(It.IsAny<Guid>())).Returns(Task.FromResult((UfDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.GetId(UfId);
            Assert.Null(_record);
        }
    }
}