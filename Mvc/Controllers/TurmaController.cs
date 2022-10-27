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

namespace Mvc.Controllers
{
    public class TurmaController : Controller
    {
        HttpClient client = new HttpClient();

        public IActionResult Index(TurmaModel turma)
        {
            client.BaseAddress = new Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("turma").Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<TurmaModel>>(dados.Result);

                return View("Index", retorno);
            }

            return View("Index", response);
        }

        
        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(TurmaModel turma)
        {

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
                return View("Index", retorno);

            }

            return View("Adicionar");

        }

        [HttpGet]
        public IActionResult Editar(TurmaModel turma)
        {
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
                return RedirectToAction("Index", response);
            }
            return View("Editar");

        }

        [HttpGet]
        public ActionResult<TurmaModel> ExcluirConfirmacao(TurmaModel turma)
        {
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
        public ActionResult Excluir(int id)
        {
            var url = ("http://localhost:22546/api/turma/" + id);
            var response = client.DeleteAsync(url).Result;

            return RedirectToAction("Index");

        }

    }
}
