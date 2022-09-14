﻿using System.ComponentModel.DataAnnotations;

namespace Teste.Models
{
	public class EscolaModel
	{
		public int Id {get; set;}

		[Required(ErrorMessage = "Campo obrigatório")]
		public string Nome {get; set;}

        public int Inep {get; set;}

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Contato {get; set;}
	}
}