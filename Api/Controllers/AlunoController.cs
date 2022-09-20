using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;

namespace Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlunoController : Controller
    {
        private readonly IAlunoRepositorio _alunoRepositorio;

        public AlunoController(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        [HttpGet("{id}")]
        public IActionResult Index()
        {
            List<AlunoModel> alunos = _alunoRepositorio.BuscarTodos();
            return View(alunos);
        }

        [HttpGet("{id}")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost("{id}")]
        public IActionResult Adicionar(AlunoModel aluno)
        {
            if (ModelState.IsValid)
            {
                _alunoRepositorio.Adicionar(aluno);

                return RedirectToAction("Index");
            }
            return View(aluno);

        }


        [HttpGet("{id}")]
        public IActionResult Editar(int id)
        {
            AlunoModel aluno = _alunoRepositorio.ListarPorId(id);
            return View(aluno);
        }

        [HttpPost("{id}")]
        public IActionResult Editar(AlunoModel aluno)
        {
            _alunoRepositorio.Atualizar(aluno);

            return RedirectToAction("Index");

        }

        [HttpGet("{id}")]
        public IActionResult ExcluirConfirmacao(int id)
        {
            AlunoModel aluno = _alunoRepositorio.ListarPorId(id);
            return View(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            _alunoRepositorio.Excluir(id);
            return RedirectToAction("Index");
        }


    }

}
