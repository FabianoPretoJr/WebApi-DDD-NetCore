using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.CEP;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QunadoForExecutadoUpdate : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Update")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Put(cepUpdateDTO)).ReturnsAsync(cepUpdateResultDTO);
            _service = _serviceMock.Object;

            var resultUpdate = await _service.Put(cepUpdateDTO);
            Assert.NotNull(resultUpdate);
            Assert.Equal(CepAtualizado, resultUpdate.Cep);
            Assert.Equal(LogradouroAtualizado, resultUpdate.Logradouro);
            Assert.Equal(NumeroAtualizado, resultUpdate.Numero);
            Assert.Equal(MunicipioId, resultUpdate.MunicipioId);
        }
    }
}