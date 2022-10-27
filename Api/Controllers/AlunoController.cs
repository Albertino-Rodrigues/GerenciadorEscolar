using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [Route("api/aluno")]
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

                var aluno = _alunoRepositorio.BuscarTodos();
                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Houve um erro: {ex.Message}.");

            }

        }

        [HttpGet("{id}")]
        public ActionResult<AlunoModel> GetResult(int id)
        {

            try
            {
                var aluno = _alunoRepositorio.ListarPorId(id);

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutResult(int id, AlunoModel alunoModel)
        {
            try
            {
                _alunoRepositorio.Atualizar(alunoModel);

                _alunoRepositorio.SaveChanges();

                var lstAluno = _alunoRepositorio.BuscarTodos();

                return Ok(lstAluno);
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

                var lstAluno = _alunoRepositorio.BuscarTodos();

                return Ok(lstAluno);


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
                _alunoRepositorio.Excluir(id);

                var aluno = _alunoRepositorio.ListarPorId(id);

                _alunoRepositorio.SaveChanges();

                return Ok(aluno);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}.");
            }
        }
    }

}


