using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;

namespace Teste.Controllers
{
    public class EscolaController : Controller
    {
        private readonly IEscolaRepositorio _escolaRepositorio;
        public EscolaController(IEscolaRepositorio escolaRepositorio)
        {
            _escolaRepositorio = escolaRepositorio;
        }
        public IActionResult Index()
        {
            List<EscolaModel> escolas = _escolaRepositorio.BuscarTodos();
            return View(escolas);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(EscolaModel escola)
        {
            if (ModelState.IsValid)
            {
                _escolaRepositorio.Adicionar(escola);

                return RedirectToAction("Index");
            }
            return View(escola);

        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            EscolaModel escola = _escolaRepositorio.ListarPorId(id);
            return View(escola);
        }

        [HttpPost]
        public IActionResult Editar(EscolaModel escola)
        {
            _escolaRepositorio.Atualizar(escola);

            return RedirectToAction("Index");

        }


        public IActionResult ExcluirConfirmacao(int id)
        {
            EscolaModel escola = _escolaRepositorio.ListarPorId(id);
            return View(escola);
        }

        public IActionResult Excluir(int id)
        {
            _escolaRepositorio.Excluir(id);
            return RedirectToAction("Index");
        }

    }
}