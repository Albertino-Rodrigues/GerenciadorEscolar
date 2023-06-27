using Biblioteca.Models;
using System.Collections.Generic;

namespace Biblioteca.Repositorio
{
    public interface ITurmaRepositorio
    {
        TurmaModel ListarPorId(int id);
        List<TurmaModel> BuscarTodos(int escolaId);
        List<TurmaModel> BuscarPorParametro(int escolaId, int? turmaId);
        TurmaModel Adicionar(TurmaModel turma);

        TurmaModel Atualizar(int id,TurmaModel turma);

        void Excluir(int id);
        bool SaveChanges();
    }
}