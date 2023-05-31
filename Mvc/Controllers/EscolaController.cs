using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace Mvc.Controllers
{
    public class EscolaController : Controller
    {
        HttpClient client = new HttpClient();


        public IActionResult Index(EscolaModel escola)
        {
            client.BaseAddress = new Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("escola").Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<EscolaModel>>(dados.Result);

                return View("Index", retorno);
            }

            return View("Index", response);
        }


        [HttpGet]
        public IActionResult Adicionar()
        {

            return View("Adicionar");
        }

        [HttpPost]
        public IActionResult Adicionar(EscolaModel escola)
        {

            client.BaseAddress = new Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var escolaSerializada = JsonConvert.SerializeObject(escola);
            var escolaContentString = new StringContent(escolaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("escola", escolaContentString).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<EscolaModel>>(dados.Result);
                return View("Index", retorno);

            }

            return View("Adicionar");

        }

        [HttpGet]
        public IActionResult Editar(EscolaModel escola)
        {
            client.BaseAddress = new Uri("http://localhost:22546/api/escola/" + escola.Id);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<EscolaModel>(dados.Result);

                return View("Editar", retorno);
            }

            return View("Editar");
        }

        public IActionResult ConfirmarEditar(EscolaModel escola)
        {
            var url = "http://localhost:22546/api/escola/" + escola.Id;

            string json = JsonConvert.SerializeObject(escola, Formatting.Indented);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var escolaJson = new ByteArrayContent(buffer);
            escolaJson.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var response = client.PutAsync(string.Format(url), escolaJson).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", response);
            }
            return View("Editar");

        }

        [HttpGet]
        public ActionResult<EscolaModel> ExcluirConfirmacao(EscolaModel escola)
        {
            client.BaseAddress = new Uri("http://localhost:22546/api/escola/" + escola.Id);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<EscolaModel>(dados.Result);

                return View("ExcluirConfirmacao", retorno);
            }

            return View("ExcluirConfirmacao");
        }
        public ActionResult Excluir(int id)
        {
            var url = ("http://localhost:22546/api/escola/" + id);
            var response = client.DeleteAsync(url).Result;

            return RedirectToAction("Index");
        }

    }

}