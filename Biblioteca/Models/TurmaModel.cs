using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class TurmaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
         
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(40)]
        public string Descricao { get; set; }

        [StringLength(20)]
        public string ComposicaoEnsino {get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(20)]
        public string AnoEscolar { get; set; }

        [ForeignKey("EscolaId")]
        public int EscolaId { get; set; }

        public virtual EscolaModel Escola { get; set; }

        public List<AlunoModel> Alunos { get; set; }

    }
}