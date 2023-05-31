using System.Collections.Generic;
using Biblioteca.Models;

namespace Biblioteca.Repositorio
{
    public interface IAlunoRepositorio
    {
        AlunoModel ListarPorId(int id);
        List<AlunoModel> BuscarTodos(int turmaId);
        AlunoModel Adicionar(AlunoModel aluno);

        AlunoModel Atualizar(int id, AlunoModel aluno);

        void Excluir(int id);

        bool SaveChanges();

    }
}
