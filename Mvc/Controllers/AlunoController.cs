using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Mvc.Controllers
{

    public class AlunoController : Controller
    {
        HttpClient client = new HttpClient();

        public IActionResult Index(AlunoModel aluno)
        {
            client.BaseAddress = new Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("aluno").Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<AlunoModel>>(dados.Result);

                return View("Index", retorno);
            }

            return View("Index", response);
        }


        [HttpGet]
        public IActionResult Adicionar(int id)
        {

            return View("Adicionar");
        }

        [HttpPost]
        public IActionResult Adicionar(AlunoModel aluno)
        {

            client.BaseAddress = new Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var alunoSerializado = JsonConvert.SerializeObject(aluno);
            var alunoContentString = new StringContent(alunoSerializado, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("aluno", alunoContentString).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<AlunoModel>>(dados.Result);
                return View("Index", retorno);

            }

            return View("Adicionar");

        }

        [HttpGet]
        public IActionResult Editar(AlunoModel aluno)
        {
            client.BaseAddress = new Uri("http://localhost:22546/api/aluno/" + aluno.Id);

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
            var url = "http://localhost:22546/api/aluno/" + aluno.Id;

            var json = JsonConvert.SerializeObject(aluno, Formatting.Indented);
            var buffer = Encoding.UTF8.GetBytes(json);
            var alunoJson = new ByteArrayContent(buffer);
            alunoJson.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var response = client.PutAsync(string.Format(url), alunoJson).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", response);
            }
            return View("Editar");

        }

        [HttpGet]
        public ActionResult<AlunoModel> ExcluirConfirmacao(AlunoModel aluno)
        {
            client.BaseAddress = new Uri("http://localhost:22546/api/aluno/" + aluno.Id);

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
        public ActionResult Excluir(int id)
        {
            var url = ("http://localhost:22546/api/aluno/" + id);
            var response = client.DeleteAsync(url).Result;

            return RedirectToAction("Index");
        }

    }


}
