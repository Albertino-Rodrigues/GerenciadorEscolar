using System;
using Biblioteca.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

namespace Biblioteca.Repositorio
{
    public class EscolaRepositorio : IEscolaRepositorio
    {
        private readonly BancoContext _context;
        

        public EscolaRepositorio(BancoContext bancoContext)
        {
            _context = bancoContext;
        }
        public EscolaModel Adicionar(EscolaModel escola)
        {
            if (escola == null)
            {
                throw new System.Exception("Houve um erro !");   

                
            }
                _context.Escolas.Add(escola);
                _context.SaveChanges();

                return escola;


        }

        public EscolaModel ListarPorId(int id)
        {
            return _context.Escolas.FirstOrDefault(x => x.Id == id);
        }


        public List<EscolaModel> BuscarTodos()
        {
            return _context.Escolas.ToList();
        }

        public EscolaModel Atualizar(EscolaModel escola)
        {
            EscolaModel escolaDB = ListarPorId(escola.Id);

            if (escolaDB == null)
            {
                throw new System.Exception("Houve um erro na atualização do contato!");
            }

            escolaDB.Nome = escola.Nome;
            escolaDB.Inep = escola.Inep;
            escolaDB.Contato = escola.Contato;


            _context.Escolas.Update(escolaDB);
            _context.SaveChanges();

            return escola;
        }

        public bool Excluir(int id)
        {
            EscolaModel escolaDB = ListarPorId(id);

            if (escolaDB == null) throw new System.Exception("Houve um erro ao tentar excluir o cadastro!");
                          
                _context.Escolas.Remove(escolaDB);
                _context.SaveChanges();

                return true;
            
        }


    }   
}