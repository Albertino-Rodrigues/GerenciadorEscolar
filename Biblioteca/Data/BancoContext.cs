using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class BancoContext : DbContext
	{
		public BancoContext(DbContextOptions<BancoContext> options) : base(options)
		{

		}

		public DbSet<EscolaModel> Escolas { get; set; }

		public DbSet<TurmaModel> Turmas { get; set; }

		public DbSet<AlunoModel> Alunos { get; set; }

	}
}
