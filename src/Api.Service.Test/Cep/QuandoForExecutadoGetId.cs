using System;
using System.Threading.Tasks;
using Api.Domain.DTO.Cep;
using Api.Domain.Interfaces.Services.CEP;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoGetId : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Get Id")]
        public async Task E_Possivel_Executar_Metodo_GetId()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(IdCep)).ReturnsAsync(cepDTO);
            _service = _serviceMock.Object;

            var result = await _service.Get(IdCep);
            Assert.NotNull(result);
            Assert.True(result.Id == IdCep);
            Assert.Equal(Cep, result.Cep);
            Assert.Equal(Logradouro, result.Logradouro);
            Assert.Equal(Numero, result.Numero);
            Assert.Equal(MunicipioId, result.MunicipioId);
            Assert.NotNull(result.Municipio);
            Assert.NotNull(result.Municipio.Uf);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((CepDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(IdCep);
            Assert.Null(_record);
        }
    }
}