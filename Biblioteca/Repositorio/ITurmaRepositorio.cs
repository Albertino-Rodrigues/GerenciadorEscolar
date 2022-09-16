using Biblioteca.Models;
using System.Collections.Generic;

namespace Biblioteca.Repositorio
{
    public interface ITurmaRepositorio
    {
        TurmaModel ListarPorId(int id);
        List<TurmaModel> BuscarTodos();
        TurmaModel Adicionar(TurmaModel turma);

        TurmaModel Atualizar(TurmaModel turma);

        bool Excluir(int id);
    }
}
