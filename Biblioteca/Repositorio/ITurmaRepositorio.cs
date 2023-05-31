using Biblioteca.Models;
using System.Collections.Generic;

namespace Biblioteca.Repositorio
{
    public interface ITurmaRepositorio
    {
        TurmaModel ListarPorId(int id);
        List<TurmaModel> BuscarTodos(int escolaId);
        TurmaModel Adicionar(TurmaModel turma);

        TurmaModel Atualizar(int id,TurmaModel turma);

        void Excluir(int id);
        bool SaveChanges();
    }
}