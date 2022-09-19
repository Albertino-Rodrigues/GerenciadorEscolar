using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;

namespace Teste.Controllers
{
    public class TurmaController : Controller
    {
        private readonly ITurmaRepositorio _turmaRepositorio;
        public TurmaController(ITurmaRepositorio turmaRepositorio)
        {
            _turmaRepositorio = turmaRepositorio;
        }
        public IActionResult Index()
        {
            List<TurmaModel> turmas = _turmaRepositorio.BuscarTodos();
            return View(turmas);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(TurmaModel turma)
        {
            if (ModelState.IsValid)
            {
                _turmaRepositorio.Adicionar(turma);

                return RedirectToAction("Index");
            }
            return View(turma);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            TurmaModel turma = _turmaRepositorio.ListarPorId(id);
            return View(turma);
        }

        [HttpPost]
        public IActionResult Editar(TurmaModel turma)
        {
            _turmaRepositorio.Atualizar(turma);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult ExcluirConfirmacao(int id)
        {
           TurmaModel turma = _turmaRepositorio.ListarPorId(id);
            return View(turma);
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            _turmaRepositorio.Excluir(id);
            return RedirectToAction("Index");
        }


    }
}
