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
        HttpClient client = new HttpClient();

        private readonly BancoContext _context;

        public EscolaRepositorio(BancoContext bancoContext)
        {
            _context = bancoContext;
        }
        public EscolaModel Adicionar(EscolaModel escola)
        {

            if (escola == null)
            {

                throw new Exception("Houve um erro !");   
              
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
                throw new Exception("Houve um erro na atualização do cadastro");
            }

            escolaDB.Nome = escola.Nome;
            escolaDB.Inep = escola.Inep;
            escolaDB.Contato = escola.Contato;


            _context.Escolas.Update(escolaDB);
            _context.SaveChanges();

            return escola;
        }

        public void Excluir(int id)
        {
            var escolaDB = ListarPorId(id);
                       
                _context.Escolas.Remove(escolaDB);
                _context.SaveChanges();


            
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }   
}