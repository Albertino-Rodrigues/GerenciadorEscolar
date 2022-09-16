using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class AlunoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int DataNasc { get; set; }
    }
}
