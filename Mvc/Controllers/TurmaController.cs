using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;

namespace Mvc.Controllers
{
    public class TurmaController : Controller
    {
        HttpClient client = new HttpClient();

        public IActionResult Index(int escolaId)
        {
            ViewBag.EscolaId = escolaId;    
            client.BaseAddress = new Uri("http://localhost:22546/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($"turma?escolaId={escolaId}").Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<TurmaModel>>(dados.Result);

                return View("Index", retorno);
            }

            return View("Index", response);
        }


        [HttpGet]
        public IActionResult Adicionar(int escolaId)
        {
            ViewBag.EscolaId = escolaId;
            var turmaModel = new TurmaModel() { EscolaId = escolaId };
            return View("Adicionar", turmaModel);
        }


        [HttpPost]
        public IActionResult Adicionar(TurmaModel turma)
        {
            ViewBag.EscolaId = turma.EscolaId;
            client.BaseAddress = new Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var turmaSerializada = JsonConvert.SerializeObject(turma);
            var turmaContentString = new StringContent(turmaSerializada, Encoding.UTF8, "application/json");
             
            HttpResponseMessage response = client.PostAsync("turma", turmaContentString).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<TurmaModel>>(dados.Result);
                return RedirectToAction("Index", new { escolaId = turma.EscolaId });

            }

            return View("Index");

        }

        [HttpGet]
        public IActionResult Editar(TurmaModel turma)
        {
            ViewBag.EscolaId = turma.EscolaId;

            client.BaseAddress = new Uri("http://localhost:22546/api/turma/" + turma.Id);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<TurmaModel>(dados.Result);

                return View("Editar", retorno);
            }

            return View("Editar");
        }


        public IActionResult ConfirmarEditar(TurmaModel turma)
        {
            var url = "http://localhost:22546/api/turma/" + turma.Id;

            string json = JsonConvert.SerializeObject(turma, Formatting.Indented);
            var buffer = Encoding.UTF8.GetBytes(json);
            var turmaJson = new ByteArrayContent(buffer);
            turmaJson.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var response = client.PutAsync(string.Format(url), turmaJson).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new { escolaId = turma.EscolaId });

            }
            return View("Editar");

        }

        [HttpGet]
        public ActionResult<TurmaModel> ExcluirConfirmacao(TurmaModel turma)
        {
            ViewBag.EscolaId = turma.EscolaId;

            client.BaseAddress = new Uri("http://localhost:22546/api/turma/" + turma.Id);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<TurmaModel>(dados.Result);

                return View("ExcluirConfirmacao", retorno);
            }

            return View("ExcluirConfirmacao");
        }
        public ActionResult Excluir(int id, TurmaModel turma)
        {
            var url = ("http://localhost:22546/api/turma/" + id);
            var response = client.DeleteAsync(url).Result;

            return RedirectToAction("Index", new { escolaId = turma.EscolaId});

        }

    }
}