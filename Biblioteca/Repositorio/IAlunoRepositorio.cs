using System.Collections.Generic;
using Biblioteca.Models;

namespace Biblioteca.Repositorio
{
    public interface IAlunoRepositorio
    {
        AlunoModel ListarPorId(int id);
        List<AlunoModel> BuscarTodos();
        AlunoModel Adicionar(AlunoModel aluno);

        AlunoModel Atualizar(AlunoModel aluno);

        bool Excluir(int id);

    }
}
