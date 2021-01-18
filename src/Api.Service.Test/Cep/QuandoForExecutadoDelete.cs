using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.CEP;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoDelete : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Delete")]
        public async Task E_Possivel_Executar_Metodo_Delete()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Delete(IdCep)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(IdCep);
            Assert.True(result);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;

            result = await _service.Delete(Guid.NewGuid());
            Assert.False(result);
        }
    }
}