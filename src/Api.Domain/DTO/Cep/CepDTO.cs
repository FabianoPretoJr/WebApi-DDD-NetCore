using System;
using Api.Domain.DTO.Municipio;

namespace Api.Domain.DTO.Cep
{
    public class CepDTO
    {
        public Guid Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public Guid MunicipioId { get; set; }
        public MunicipioCompletoDTO Municipio { get; set; }
    }
}