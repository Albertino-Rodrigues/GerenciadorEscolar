using System;
using Biblioteca.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

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

                throw new Exception("Houve um erro ao adicionar!");   
              
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

        public EscolaModel Atualizar(int id, EscolaModel escola)
        {
            EscolaModel escolaDB = ListarPorId(id);

            if (escolaDB == null)
            {
                throw new Exception("Houve um erro na atualização do cadastro");
            }

            escolaDB.Nome = escola.Nome;
            escolaDB.Inep = escola.Inep;
            escolaDB.Contato = escola.Contato;


            _context.Escolas.Update(escolaDB);
            _context.SaveChanges();

            return escolaDB;
        }

        public void Excluir(int id)
        {
            var escolaDB = ListarPorId(id);
                       
                _context.Escolas.Remove(escolaDB);
            
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }   
}