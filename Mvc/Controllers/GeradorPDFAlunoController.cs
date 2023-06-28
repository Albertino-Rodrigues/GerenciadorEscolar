using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Table = iText.Layout.Element.Table;
using System.Threading.Tasks;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.Linq;
using iText.Kernel.Geom;
using System.Reflection;
using iText.Layout.Properties;
using Microsoft.Build.Tasks;

namespace Mvc.Controllers
{
    public class GeradorPDFAlunoController : Controller
    {
        HttpClient client = new HttpClient();

        private async Task<EscolaModel> ResponseEscolas(int escolaId)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:14708/api/");

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseEscola = await client.GetAsync($"escola/{escolaId}");

                if (responseEscola.IsSuccessStatusCode)
                {
                    var dados = await responseEscola.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<EscolaModel>(dados);
                }

                return null;
            }
        }

        private async Task<List<TurmaModel>> ResponseTurmas(int escolaId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14708/api/turma/obtenha");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseTurma = await client.GetAsync($"?escolaId={escolaId}");

                if (responseTurma.IsSuccessStatusCode)
                {
                    var dados = await responseTurma.Content.ReadAsStringAsync();
                    var listaTurmas = JsonConvert.DeserializeObject<List<TurmaModel>>(dados);

                    return listaTurmas;
                }

                return new List<TurmaModel>();
            }
        }

        private async Task<List<AlunoModel>> ResponseAlunos(int turmaId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14708/api/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseAluno = await client.GetAsync($"aluno?turmaId={turmaId}");

                if (responseAluno.IsSuccessStatusCode)
                {
                    var dados = await responseAluno.Content.ReadAsStringAsync();
                    var listaAlunos = JsonConvert.DeserializeObject<List<AlunoModel>>(dados);
                    return listaAlunos;
                }

                return new List<AlunoModel>();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index(int escolaId, int? turmaId)
        {
            var escola = await ResponseEscolas(escolaId);
            var listaTurmas = await ResponseTurmas(escolaId);

            if (listaTurmas is List<TurmaModel> turmas)
            {
                try
                {
                    string caminhoDoArquivo = @"C:\arquivo.pdf";
                    PdfWriter writer = new PdfWriter(caminhoDoArquivo);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf, PageSize.A4, false);


                    Style tituloStyle = new Style()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(18);

                    Style turmaStyle = new Style()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(14)
                        .SetBold();


                    document.Add(new Paragraph("Lista de alunos").AddStyle(tituloStyle));
                    document.Add(new Paragraph(escola.Nome).AddStyle(tituloStyle));


                    foreach (TurmaModel turma in turmas.OrderBy(c => c.Descricao))
                    {
                        var nomeTurma =  turma.Descricao;
                        document.Add(new Paragraph("Turma: " + nomeTurma).AddStyle(turmaStyle));

                        document.Add(new Paragraph("\n"));

                        var alunos = await ResponseAlunos(turma.Id);
                        foreach (AlunoModel aluno in alunos)
                        {
                            Paragraph nomeAluno = new Paragraph("Nome: " + aluno.Nome);
                            document.Add(nomeAluno);

                            Table tabelaInformacoes = new Table(1);
                            tabelaInformacoes.AddCell("CPF: " + aluno.Cpf);
                            tabelaInformacoes.AddCell("Data de nascimento: " + aluno.DataNasc);

                            document.Add(tabelaInformacoes);

                            document.Add(new Paragraph("\n"));
                        }
                    }


                    document.Close();

                    byte[] fileBytes = System.IO.File.ReadAllBytes(caminhoDoArquivo);

                    var contentDisposition = new ContentDispositionHeaderValue("inline");
                    Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

                    return File(fileBytes, "application/pdf");
                }
                catch
                {
                    throw new Exception("Ocorreu um erro ao gerar o relatório");
                }
            }
            else
            {
                return BadRequest("Ocorreu um erro ao gerar o relatório");
            }
        }




    }

}

