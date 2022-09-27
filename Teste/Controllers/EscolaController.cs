using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;

namespace Teste.Controllers
{
    public class EscolaController : Controller
    {
        HttpClient client = new HttpClient();

        private readonly IEscolaRepositorio _escolaRepositorio;
        public EscolaController(IEscolaRepositorio escolaRepositorio)
        {
            _escolaRepositorio = escolaRepositorio;
        }
       public IActionResult Index()
       {
           var escolas = _escolaRepositorio.BuscarTodos();
           return View(escolas);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<EscolaModel>> Adicionar(EscolaModel escola)
        {

            client.BaseAddress = new System.Uri("http://localhost:5000/Escolas");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/escolas");

            if (response.IsSuccessStatusCode)
            {
                var dados =  await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EscolaModel>>(dados);
            }

            return new List<EscolaModel>();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var escola = _escolaRepositorio.ListarPorId(id);
            return View(escola);
        }

        [HttpPost]
        public IActionResult Editar(EscolaModel escola)
        {
            if(ModelState.IsValid)
            {
                _escolaRepositorio.Atualizar(escola);

                return RedirectToAction("Index");
            }

            return View(escola);
          
        }


        public IActionResult ExcluirConfirmacao(int id)
        {
            var escola = _escolaRepositorio.ListarPorId(id);
            return View(escola);
        }

        public IActionResult Excluir(int id)
        {
            _escolaRepositorio.Excluir(id);
            return RedirectToAction("Index");
        }

    }
}