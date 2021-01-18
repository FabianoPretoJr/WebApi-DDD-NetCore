using System;
using System.Threading.Tasks;
using Api.Domain.DTO.Cep;
using Api.Domain.Interfaces.Services.CEP;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoGetCep : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Get Cep")]
        public async Task E_Possivel_Executar_Metodo_GetCep()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(Cep)).ReturnsAsync(cepDTO);
            _service = _serviceMock.Object;

            var result = await _service.Get(Cep);
            Assert.NotNull(result);
            Assert.True(result.Id == IdCep);
            Assert.Equal(Cep, result.Cep);
            Assert.Equal(Logradouro, result.Logradouro);
            Assert.Equal(Numero, result.Numero);
            Assert.Equal(MunicipioId, result.MunicipioId);
            Assert.NotNull(result.Municipio);
            Assert.NotNull(result.Municipio.Uf);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<string>())).Returns(Task.FromResult((CepDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(Cep);
            Assert.Null(_record);
        }
    }
}