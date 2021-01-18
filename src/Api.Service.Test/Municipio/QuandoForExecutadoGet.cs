using System;
using System.Threading.Tasks;
using Api.Domain.DTO.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGet : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Get Id")]
        public async Task E_Possivel_Executar_Metodo_GetId()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetId(IdMunicipio)).ReturnsAsync(municipioDTO);
            _service = _serviceMock.Object;

            var result = await _service.GetId(IdMunicipio);
            Assert.NotNull(result);
            Assert.True(result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            Assert.Equal(UfId, result.UfId);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetId(It.IsAny<Guid>())).Returns(Task.FromResult((MunicipioDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.GetId(IdMunicipio);
            Assert.Null(_record);
        }
    }
}