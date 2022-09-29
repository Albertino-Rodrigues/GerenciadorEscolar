using Biblioteca.Models;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Data;

namespace Biblioteca.Repositorio
{

    public class TurmaRepositorio : ITurmaRepositorio
        {
            private readonly BancoContext _context;


            public TurmaRepositorio(BancoContext bancoContext)
            {
                _context = bancoContext;
            }
            public TurmaModel Adicionar(TurmaModel turma)
            {
                if (turma == null)
                {
                    throw new System.Exception("Houve um erro!");


                }
                _context.Turmas.Add(turma);
                _context.SaveChanges();

                return turma;


            }

            public TurmaModel ListarPorId(int id)
            {
                return _context.Turmas.FirstOrDefault(x => x.Id == id);
            }


            public List<TurmaModel> BuscarTodos()
            {
                return _context.Turmas.ToList();
            }

            public TurmaModel Atualizar(TurmaModel turma)
            {
                TurmaModel turmaDB = ListarPorId(turma.Id);

                if (turmaDB == null)
                {
                    throw new System.Exception("Houve um erro na atualização do contato!");
                }

                turmaDB.Descricao = turma.Descricao;
                turmaDB.ComposicaoEnsino = turma.ComposicaoEnsino;
                turmaDB.AnoEscolar = turma.AnoEscolar;


                _context.Turmas.Update(turmaDB);
                _context.SaveChanges();

                return turma;
            }

            public bool Excluir(int id)
            {
                TurmaModel turmaDB = ListarPorId(id);

                if (turmaDB == null) throw new System.Exception("Houve um erro ao tentar excluir o cadastro!");

                _context.Turmas.Remove(turmaDB);
                _context.SaveChanges();

                return true;

            }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
    
}
