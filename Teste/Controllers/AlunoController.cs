using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;

namespace Biblioteca.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        public AlunoController(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }
        public IActionResult Index()
        {
            List<AlunoModel> alunos = _alunoRepositorio.BuscarTodos();
            return View(alunos);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(AlunoModel aluno)
        {
            if (ModelState.IsValid)
            {
                _alunoRepositorio.Adicionar(aluno);

                return RedirectToAction("Index");
            }
            return View(aluno);

        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            AlunoModel aluno = _alunoRepositorio.ListarPorId(id);
            return View(aluno);
        }

        [HttpPost]
        public IActionResult Editar(AlunoModel aluno)
        {
            _alunoRepositorio.Atualizar(aluno);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult ExcluirConfirmacao(int id)
        {
            AlunoModel aluno = _alunoRepositorio.ListarPorId(id);
            return View(aluno);
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            _alunoRepositorio.Excluir(id);
            return RedirectToAction("Index");
        }





    }
}
