using Microsoft.EntityFrameworkCore;
using Teste.Models;

namespace Teste.Data
{
	public class BancoContext : DbContext
	{
		public BancoContext(DbContextOptions<BancoContext> options) : base(options)
		{

		}

		public DbSet<EscolaModel> Escolas { get; set; }

		public DbSet<TurmaModel> Turmas { get; set; }

	}
}
