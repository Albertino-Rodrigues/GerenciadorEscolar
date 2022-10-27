using System.ComponentModel.DataAnnotations;


namespace Biblioteca.Models
{
	public class EscolaModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]

		public string Nome { get; set; }
        //[Required(ErrorMessage = "Campo obrigatório")]
        
		public int Inep { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]
		public string Contato { get; set; }
	}
}
