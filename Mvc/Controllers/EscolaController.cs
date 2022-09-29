using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace Mvc.Controllers
{
    public class EscolaController : Controller
    {
        HttpClient client = new HttpClient();


       public async Task<List<EscolaModel>> IndexAsync(EscolaModel escola)
       {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var escolaSerializada = JsonConvert.SerializeObject(escola);
            var escolaContentString = new StringContent(escolaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.GetAsync("escola");

            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EscolaModel>>(dados);
            }

            return new List<EscolaModel>();

       }


        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<EscolaModel>> Adicionar(EscolaModel escola)
        {

            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var escolaSerializada = JsonConvert.SerializeObject(escola);
            var escolaContentString = new StringContent(escolaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("escola", escolaContentString);

            if (response.IsSuccessStatusCode)
            {
                var dados = await  response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EscolaModel>>(dados);

            }

            return new List<EscolaModel> { escola };
        }

        [HttpGet]
        public async Task<List<EscolaModel>> Editar(EscolaModel escola)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var escolaSerializada = JsonConvert.SerializeObject(escola);
            var escolaContentString = new StringContent(escolaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.GetAsync("escola");

            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EscolaModel>>(dados);
            }

            return new List<EscolaModel> {};
        }

        [HttpPut]
        public void  Editar(int id)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var escolaSerializada = JsonConvert.SerializeObject(escola);
            var escolaContentString = new StringContent(escolaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response =  client.PutAsync("escola", escolaContentString).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(response.Result);

            }

        }

        public void ExcluirConfirmacao(int id)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var escolaSerializada = JsonConvert.SerializeObject(escola);
            var escolaContentString = new StringContent(escolaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.GetAsync("escola").Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EscolaModel>>(dados);
            }

            return new List<EscolaModel>();
        }

        public void Excluir(int id)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");
            var response = client.DeleteAsync(client.BaseAddress).Result;

        }

    }
}