using System;
using Api.Domain.DTO.Uf;

namespace Api.Domain.DTO.Municipio
{
    public class MunicipioCompletoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int CodIBGE { get; set; }
        public Guid UfId { get; set; }
        public UfDTO Uf { get; set; }
    }
}