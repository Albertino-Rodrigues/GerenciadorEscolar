using Biblioteca.Models;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Data;
using Microsoft.EntityFrameworkCore;
using System;

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
            //turma.Id = 0;
            var escola = _context.Escolas.FirstOrDefault(e => e.Id == turma.EscolaId);
            if (turma == null)
            {
                throw new System.Exception("Houve um erro!");

                
            }
            //turma.EscolaId = escolaId;
            _context.Turmas.Add(turma);
            _context.SaveChanges();

            return turma;


        }

        public TurmaModel ListarPorId(int id)
        {
            return _context.Turmas.FirstOrDefault(x => x.Id == id);
        }


        public List<TurmaModel> BuscarTodos(int escolaId)
        {
            return _context.Turmas.Where(x => x.EscolaId == escolaId).ToList();
        }

        public List<TurmaModel> BuscarPorParametro(int escolaId, int? turmaId)
        {
            if (turmaId.HasValue)
                return _context.Turmas.Where(x => x.EscolaId == escolaId && x.Id == turmaId).ToList();

            return _context.Turmas.Where(x => x.EscolaId == escolaId).ToList();
        }

        public TurmaModel Atualizar(int id, TurmaModel turma)
        {
            TurmaModel turmaDB = ListarPorId(id);
            //turma = ListarPorId(id);

            if (turmaDB == null)
            {
                throw new System.Exception("Houve um erro na atualização do contato!");
            }

            turmaDB.Descricao = turma.Descricao;
            turmaDB.ComposicaoEnsino = turma.ComposicaoEnsino;
            turmaDB.AnoEscolar = turma.AnoEscolar;
    


            _context.Turmas.Update(turmaDB);
            _context.SaveChanges();

            return turmaDB;
        }

        public void Excluir(int id)
        {
            var turmaDB = ListarPorId(id);

            if (turmaDB == null) throw new System.Exception("Houve um erro ao tentar excluir o cadastro!");

            _context.Turmas.Remove(turmaDB);
           // _context.SaveChanges();

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }

}