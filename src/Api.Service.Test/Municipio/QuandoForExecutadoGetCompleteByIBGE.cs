using System.Threading.Tasks;
using Api.Domain.DTO.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGetCompleteByIBGE : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Get Complete By IBGE")]
        public async Task E_Possivel_Executar_Metodo_GetCompleteByIBGE()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompleteByIBGE(CodigoIBGEMunicipio)).ReturnsAsync(municipioCompletoDTO);
            _service = _serviceMock.Object;

            var result = await _service.GetCompleteByIBGE(CodigoIBGEMunicipio);
            Assert.NotNull(result);
            Assert.True(result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            Assert.Equal(UfId, result.UfId);
            Assert.NotNull(result.Uf);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompleteByIBGE(It.IsAny<int>())).Returns(Task.FromResult((MunicipioCompletoDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.GetCompleteByIBGE(It.IsAny<int>());
            Assert.Null(_record);
        }
    }
}