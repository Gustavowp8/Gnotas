using System.ComponentModel.DataAnnotations;

namespace Gnotas.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail e obrigatorio")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha e obrigatorio")]
        public string? Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
