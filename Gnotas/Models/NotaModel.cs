using System.ComponentModel.DataAnnotations;

namespace Gnotas.Models
{
    public class NotaModel
    {
        [Key]
        public int IdNota { get; set; }

        [Required(ErrorMessage = "Titulo e obrigatorio"), MaxLength(20, ErrorMessage = "Apenas 20 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Insira o conteudo da nota"), MaxLength(250, ErrorMessage = "Apenas 250 caracteres")]
        public string Descricao { get; set; }
    }
}
