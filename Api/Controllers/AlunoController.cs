using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepositorio _alunoRepositorio;

        public AlunoController(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlunoModel>> GetResult()
        {

            try
            {
                return _alunoRepositorio.BuscarTodos();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}.");

            }

        }

        [HttpGet("{id}")]
        public ActionResult<AlunoModel> GetResult(int id)
        {
            try
            {
                var aluno = _alunoRepositorio.ListarPorId(id);

                return aluno;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutResult(int id, AlunoModel aluno)
        {

            try
            {
                _alunoRepositorio.Atualizar(aluno);

                _alunoRepositorio.SaveChanges();

                return Ok($"{aluno.Nome} atualizado.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}");
            }


        }

        [HttpPost]
        public ActionResult<AlunoModel> PostResult(AlunoModel aluno)
        {
            try
            {
                _alunoRepositorio.Adicionar(aluno);
                _alunoRepositorio.SaveChanges();

                return CreatedAtAction("GetResult", new { id = aluno.Id }, aluno);


            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro:{ex.Message}.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<AlunoModel> DeleteResult(int id)
        {

            try
            {            
                
                var aluno = _alunoRepositorio.ListarPorId(id);
                _alunoRepositorio.Excluir(id);
 
                _alunoRepositorio.SaveChanges();

                return Ok($"{aluno.Nome} deletado.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}.");
            }
        }



    }

}
