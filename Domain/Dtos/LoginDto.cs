using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O campo email é obrigatório para o login.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [StringLength(100, ErrorMessage = "Email deve conter menos de {1} caracteres. ")]
        public string Email { get; set; }
    }
}
