using System.ComponentModel.DataAnnotations;

namespace Gnotas.Models
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Nome"), MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "Senhas são diferentes")]
        public string ConfirmPassword { get; set; }
    }

}

