﻿using Biblioteca.Models;
using System.Collections.Generic;

namespace Biblioteca.Repositorio
{
    public interface IEscolaRepositorio
    {
        EscolaModel ListarPorId(int id);
        List<EscolaModel> BuscarTodos();
        EscolaModel Adicionar(EscolaModel escola);

        EscolaModel Atualizar(int id, EscolaModel escola);

        bool SaveChanges();

        void Excluir(int id);
        
    }
}
