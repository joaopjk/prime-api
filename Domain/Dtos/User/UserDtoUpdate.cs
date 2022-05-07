using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdate
    {
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(100, ErrorMessage = " O nome deve conter no máximo {1} caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email informado é inválido.")]
        public string Email { get; set; }
    }
}
