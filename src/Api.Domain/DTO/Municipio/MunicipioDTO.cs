using System;

namespace Api.Domain.DTO.Municipio
{
    public class MunicipioDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int CodIBGE { get; set; }
        public Guid UfId { get; set; }
    }
}