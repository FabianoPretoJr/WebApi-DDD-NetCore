using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTO.Cep
{
    public class CepUpdateDTO
    {
        [Required(ErrorMessage = "Id é campo orbigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "CEP é campo obrigatório")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro é campo obrigatório")]
        public string Logradouro { get; set; }

        public string Numero { get; set; }

        [Required(ErrorMessage = "Municipio é campo obrigatório")]
        public Guid MunicipioId { get; set; }
    }
}