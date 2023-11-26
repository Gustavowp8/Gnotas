using System.ComponentModel.DataAnnotations;

namespace Gnotas.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "O nome de usuário não está disponível")]
        [Display(Name = "Nome"), MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "A senha deve conter de 8 a 15 caracteres, incluindo pelo menos uma letra maiúscula, uma letra minúscula e um número.")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "Senhas são diferentes")]
        public string ConfirmPassword { get; set; }
    }

}

