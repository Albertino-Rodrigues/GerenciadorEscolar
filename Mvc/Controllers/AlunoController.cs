using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Controllers
{

    public class AlunoController : Controller
    {
        HttpClient client = new HttpClient();

        public async Task<List<AlunoModel>> IndexAsync(AlunoModel aluno)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var alunoSerializado = JsonConvert.SerializeObject(aluno);
            var alunoContentString = new StringContent(alunoSerializado, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.GetAsync("aluno");

            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AlunoModel>>(dados);
            }

            return new List<AlunoModel>();

        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<AlunoModel>> Adicionar(AlunoModel aluno)
        {

            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var alunoSerializado = JsonConvert.SerializeObject(aluno);
            var alunoContentString = new StringContent(alunoSerializado, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("aluno", alunoContentString);

            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AlunoModel>>(dados);

            }

            return new List<AlunoModel> { aluno };
        }

        [HttpGet]
        public async Task<List<AlunoModel>> Editar(AlunoModel aluno)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var alunoSerializado = JsonConvert.SerializeObject(aluno);
            var alunoContentString = new StringContent(alunoSerializado, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.GetAsync("aluno");

            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AlunoModel>>(dados);
            }

            return new List<AlunoModel> { };
        }

        [HttpPut]
        public void Editar(int id)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var alunoSerializado = JsonConvert.SerializeObject(aluno);
            var alunoContentString = new StringContent(alunoSerializado, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync("aluno", alunoContentString).Result;

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

            var alunoSerializado = JsonConvert.SerializeObject(aluno);
            var alunoContentString = new StringContent(alunoSerializado, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.GetAsync("aluno").Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AlunoModel>>(dados);
            }

        }

        public void Excluir(int id)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");
            var response = client.DeleteAsync(client.BaseAddress).Result;

        }

    }
}