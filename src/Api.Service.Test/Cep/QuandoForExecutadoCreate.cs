using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.CEP;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoCreate : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Create")]
        public async Task E_Possivel_Executar_Metodo_Create()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Post(cepCreateDTO)).ReturnsAsync(cepCreateResultDTO);
            _service = _serviceMock.Object;

            var result = await _service.Post(cepCreateDTO);
            Assert.NotNull(result);
            Assert.Equal(Cep, result.Cep);
            Assert.Equal(Logradouro, result.Logradouro);
            Assert.Equal(Numero, result.Numero);
            Assert.Equal(MunicipioId, result.MunicipioId);
        }
    }
}