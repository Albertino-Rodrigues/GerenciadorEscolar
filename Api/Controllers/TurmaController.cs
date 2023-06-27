using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using Microsoft.AspNetCore.Http;
using System;

namespace Api.Controllers
{
    [Route("api/turma")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaRepositorio _turmaRepositorio;
        public TurmaController(ITurmaRepositorio turmaRepositorio)
        {
            _turmaRepositorio = turmaRepositorio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TurmaModel>> GetResult(int escolaId)
        {

            try
            {
                
                var turma = _turmaRepositorio.BuscarTodos(escolaId);
                return Ok(turma);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Houve um erro: {ex.Message}.");

            }

        }

        [HttpGet]
        [Route("obtenha")]
        public ActionResult<IEnumerable<TurmaModel>> ObtenhaTurmasPorParametro([FromQuery]int escolaId, [FromQuery] int? turmaId)
        {
            try
            {
                var turma = _turmaRepositorio.BuscarPorParametro(escolaId, turmaId);
                return Ok(turma);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Houve um erro: {ex.Message}.");

            }

        }

        [HttpGet("{id}")]
        public ActionResult<TurmaModel> GetResultPorId(int id)
        {

            try
            {
                var turma = _turmaRepositorio.ListarPorId(id);

                return Ok(turma);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutResult(int id, TurmaModel turma, int escolaId)
        {
            try
            {
                _turmaRepositorio.Atualizar(id, turma);

                _turmaRepositorio.SaveChanges();

                var lstTurma = _turmaRepositorio.BuscarTodos(escolaId);

                return Ok(lstTurma);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}");
            }


        }

        [HttpPost]
        public ActionResult<TurmaModel> PostResult(TurmaModel turma, int escolaId)
        {
            try
            {

                _turmaRepositorio.Adicionar(turma);
                _turmaRepositorio.SaveChanges();

                var lstTurma = _turmaRepositorio.BuscarTodos(escolaId);

                return Ok(lstTurma);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro:{ex.Message}.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<TurmaModel> DeleteResult(int id)
        {

            try
            {
                _turmaRepositorio.Excluir(id);

                var lstTurma = _turmaRepositorio.ListarPorId(id);

                _turmaRepositorio.SaveChanges();

                return Ok($"{ lstTurma} excluida com sucesso");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}.");
            }
        }

    }

}
