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


        public List<AlunoModel> BuscarTodos(int turmaId)
        {
            return _context.Alunos.Where(x => x.TurmaId == turmaId).ToList();
        }

        public AlunoModel Atualizar(int id, AlunoModel aluno)
        {
            AlunoModel alunoDB = ListarPorId(id);

            if (alunoDB == null)
            {
                throw new System.Exception("Houve um erro na atualização do cadastro!");
            }

            alunoDB.Nome = aluno.Nome;
            alunoDB.Cpf = aluno.Cpf;
            alunoDB.DataNasc = aluno.DataNasc;


            _context.Alunos.Update(alunoDB);
            _context.SaveChanges();

            return alunoDB;
        }

        public void Excluir(int id)
        {
            var alunoDB = ListarPorId(id);

            if (alunoDB == null) throw new System.Exception("Houve um erro ao tentar excluir o cadastro!");

            _context.Alunos.Remove(alunoDB);

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
