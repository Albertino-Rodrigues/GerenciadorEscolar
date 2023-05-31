using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class EscolaModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o INEP")]
        [Display(Name = "INEP")]
        [StringLength(8)]
        [MaxLength(8)]
        public string Inep { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Contato { get; set; }

        public List<TurmaModel> Turmas { get; set; }
    }
}