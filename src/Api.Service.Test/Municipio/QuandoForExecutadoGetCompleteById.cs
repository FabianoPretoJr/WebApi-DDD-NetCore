using System;
using System.Threading.Tasks;
using Api.Domain.DTO.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGetCompleteById : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Get Complete By Id")]
        public async Task E_Possivel_Executar_Metodo_GetCompleteById()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompleteById(IdMunicipio)).ReturnsAsync(municipioCompletoDTO);
            _service = _serviceMock.Object;

            var result = await _service.GetCompleteById(IdMunicipio);
            Assert.NotNull(result);
            Assert.True(result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            Assert.Equal(UfId, result.UfId);
            Assert.NotNull(result.Uf);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompleteById(It.IsAny<Guid>())).Returns(Task.FromResult((MunicipioCompletoDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.GetCompleteById(It.IsAny<Guid>());
            Assert.Null(_record);
        }
        
    }
}