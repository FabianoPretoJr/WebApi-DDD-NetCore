using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTO.User
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "Id é campo obrigatório")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Nome é campo obrigatório")]
        [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nome é campo obrigatório")]
        [StringLength(100, ErrorMessage = "E-mail deve ter no máximo {1} caracteres")]
        [EmailAddress(ErrorMessage = "e-mail em formato inválido")]
        public string Email { get; set; }
    }
}