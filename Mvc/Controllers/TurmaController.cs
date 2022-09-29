using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Controllers
{
    public class TurmaController : Controller
    {
        HttpClient client = new HttpClient();

        public async Task<List<TurmaModel>> IndexAsync(TurmaModel turma)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var turmaSerializada = JsonConvert.SerializeObject(turma);
            var turmaContentString = new StringContent(turmaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.GetAsync("turma");

            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TurmaModel>>(dados);
            }

            return new List<TurmaModel>();
        }

        
        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<TurmaModel>> Adicionar(TurmaModel turma)
        {

            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var turmaSerializada = JsonConvert.SerializeObject(turma);
            var turmaContentString = new StringContent(turmaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("turma", turmaContentString);

            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TurmaModel>>(dados);

            }
            
            return new List<TurmaModel> { turma };
        }

        [HttpGet]
        public async Task<List<TurmaModel>> Editar(TurmaModel turma)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var turmaSerializada = JsonConvert.SerializeObject(turma);
            var turmaContentString = new StringContent(turmaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.GetAsync("turma");

            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TurmaModel>>(dados);
            }

            return new List<TurmaModel> { turma };
        }

        [HttpPut]
        public void Editar(int id)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var turmaSerializada = JsonConvert.SerializeObject(turma);
            var turmaContentString = new StringContent(turmaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync("turma", turmaContentString).Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(response.Result);

            }

        }

        [HttpGet]
        public void ExcluirConfirmacao(int id)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var turmaSerializada = JsonConvert.SerializeObject(turma);
            var turmaContentString = new StringContent(turmaSerializada, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.GetAsync("turma").Result;

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TurmaModel>>(dados);
            }

        }
        public void Excluir(int id)
        {
            client.BaseAddress = new System.Uri("http://localhost:22546/api/");
            var response = client.DeleteAsync(client.BaseAddress).Result;

        }

    }
}
