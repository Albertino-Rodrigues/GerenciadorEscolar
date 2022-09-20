using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Repositorio;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EscolaController : Controller
    {
        private readonly IEscolaRepositorio _escolaRepositorio;
        public EscolaController(IEscolaRepositorio escolaRepositorio)
        {
            _escolaRepositorio = escolaRepositorio;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<EscolaModel> escolas = _escolaRepositorio.BuscarTodos();
            return View(escolas);
        }

        [HttpGet("{id}")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost("{id}")]
        public IActionResult Adicionar(EscolaModel escola)
        {
            if (ModelState.IsValid)
            {
                _escolaRepositorio.Adicionar(escola);

                return RedirectToAction("Index");
            }
            return View(escola);

        }

        [HttpGet("{id}")]
        public IActionResult Editar(int id)
        {
            EscolaModel escola = _escolaRepositorio.ListarPorId(id);
            return View(escola);
        }


        [HttpPost("{id}")]
        public IActionResult Editar(EscolaModel escola)
        {
            _escolaRepositorio.Atualizar(escola);

            return RedirectToAction("Index");

        }

        [HttpGet("{id}")]
        public IActionResult ExcluirConfirmacao(int id)
        {
            EscolaModel escola = _escolaRepositorio.ListarPorId(id);
            return View(escola);
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            _escolaRepositorio.Excluir(id);
            return RedirectToAction("Index");
        }

    }
}