using System.Collections.Generic;
using System.Linq;
using Biblioteca.Data;
using Biblioteca.Models;

namespace Biblioteca.Repositorio
{
    public class AlunoRepositorio : IAlunoRepositorio
    {

        private readonly BancoContext _context;


        public AlunoRepositorio(BancoContext bancoContext)
        {
            _context = bancoContext;
        }
        public AlunoModel Adicionar(AlunoModel aluno)
        {
            if (aluno == null)
            {
                throw new System.Exception("Houve um erro !");


            }
            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            return aluno;


        }

        public AlunoModel ListarPorId(int id)
        {
            return _context.Alunos.FirstOrDefault(x => x.Id == id);
        }


        public List<AlunoModel> BuscarTodos()
        {
            return _context.Alunos.ToList();
        }

        public AlunoModel Atualizar(AlunoModel aluno)
        {
            AlunoModel alunoDB = ListarPorId(aluno.Id);

            if (alunoDB == null)
            {
                throw new System.Exception("Houve um erro na atualização do cadastro!");
            }

            alunoDB.Nome = aluno.Nome;
            alunoDB.Cpf = aluno.Cpf;
            alunoDB.DataNasc = aluno.DataNasc;


            _context.Alunos.Update(alunoDB);
            _context.SaveChanges();

            return aluno;
        }

        public bool Excluir(int id)
        {
            AlunoModel alunoDB = ListarPorId(id);

            if (alunoDB == null) throw new System.Exception("Houve um erro ao tentar excluir o cadastro!");

            _context.Alunos.Remove(alunoDB);
            _context.SaveChanges();

            return true;

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
