using System.ComponentModel.DataAnnotations;

namespace Teste.Models
{
    public class TurmaModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Descricao { get; set; }

        public string ComposicaoEnsino { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string AnoEscolar { get; set; }

    }
}

