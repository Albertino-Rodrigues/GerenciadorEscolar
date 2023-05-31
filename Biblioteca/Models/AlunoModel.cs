using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class AlunoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime DataNasc { get; set; }

        [ForeignKey("EscolaId")]
        public int TurmaId { get; set; }
        public TurmaModel Turma { get; set; }
    }
}