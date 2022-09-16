using Biblioteca.Models;
using System.Collections.Generic;

namespace Biblioteca.Repositorio
{
    public interface IEscolaRepositorio
    {
        EscolaModel ListarPorId(int id);
        List<EscolaModel> BuscarTodos();
        EscolaModel Adicionar(EscolaModel escola);

        EscolaModel Atualizar(EscolaModel escola);

        bool Excluir(int id);
        
    }
}
