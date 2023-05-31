using Biblioteca.Models;
using Biblioteca.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TurmaModel>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
  
        }


    }
}
