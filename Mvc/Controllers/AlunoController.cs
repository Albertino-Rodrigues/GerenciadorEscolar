using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System;
using iText.Kernel.Pdf;
using System.IO;
using iText.Layout;
using iText.Layout.Element;

namespace Mvc.Controllers
{

    public class AlunoController : Controller
    {
        HttpClient client = new HttpClient();

        public IActionResult Index(int escolaId, int turmaId)
        {
            ViewBag.EscolaId = escolaId;
            ViewBag.TurmaId = turmaId;
            client.BaseAddress = new Uri("http://localhost:14708/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($"aluno?turmaId={turmaId}").Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<AlunoModel>>(dados.Result);

                return View("Index", retorno);
            }

            return View("Index", response);
        }


        [HttpGet]
        public IActionResult Adicionar(int turmaId)
        {
            ViewBag.TurmaId = turmaId;
            var alunoModel = new AlunoModel() { TurmaId = turmaId };
            return View("Adicionar", alunoModel);
        }

        [HttpPost]
        public IActionResult Adicionar(AlunoModel aluno)
        {
            ViewBag.TurmaId = aluno.TurmaId;

            client.BaseAddress = new Uri("http://localhost:14708/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var alunoSerializado = JsonConvert.SerializeObject(aluno);
            var alunoContentString = new StringContent(alunoSerializado, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("aluno", alunoContentString).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<AlunoModel>>(dados.Result);
                return RedirectToAction("Index", new { turmaId = aluno.TurmaId });

            }

            return View("Adicionar");

        }

        [HttpGet]
        public IActionResult Editar(AlunoModel aluno)
        {
            ViewBag.TurmaId = aluno.TurmaId;
            client.BaseAddress = new Uri("http://localhost:14708/api/aluno/" + aluno.Id);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<AlunoModel>(dados.Result);

                return View("Editar", retorno);
            }

            return View("Editar");
        }

        public IActionResult ConfirmarEditar(AlunoModel aluno)
        {
            var url = "http://localhost:14708/api/aluno/" + aluno.Id;

            var json = JsonConvert.SerializeObject(aluno, Formatting.Indented);
            var buffer = Encoding.UTF8.GetBytes(json);
            var alunoJson = new ByteArrayContent(buffer);
            alunoJson.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var response = client.PutAsync(string.Format(url), alunoJson).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new {turmaId = aluno.TurmaId});
            }
            return View("Editar");

        }

        [HttpGet]
        public ActionResult<AlunoModel> ExcluirConfirmacao(AlunoModel aluno)
        {
            ViewBag.TurmaId = aluno.TurmaId;

            client.BaseAddress = new Uri("http://localhost:14708/api/aluno/" + aluno.Id);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<AlunoModel>(dados.Result);

                return View("ExcluirConfirmacao", retorno);
            }

            return View("ExcluirConfirmacao");
        }
        public ActionResult Excluir(int id, AlunoModel aluno)
        {
            var url = ("http://localhost:14708/api/aluno/" + id);
            var response = client.DeleteAsync(url).Result;

            return RedirectToAction("Index", new { turmaId = aluno.TurmaId });
        }

    }


}