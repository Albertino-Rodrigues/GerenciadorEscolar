using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using Biblioteca.Repositorio;
using Microsoft.AspNetCore.Http;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaRepositorio _turmaRepositorio;
        public TurmaController(ITurmaRepositorio turmaRepositorio)
        {
            _turmaRepositorio = turmaRepositorio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TurmaModel>> GetResult()
        {

            try
            {
                return _turmaRepositorio.BuscarTodos();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}.");

            }

        }

        [HttpGet("{id}")]
        public ActionResult<TurmaModel> GetResult(int id)
        {
            try
            {
                var turma = _turmaRepositorio.ListarPorId(id);

                return turma;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutResult(int id, TurmaModel turma)
        {

            try
            {
                _turmaRepositorio.Atualizar(turma);

                _turmaRepositorio.SaveChanges();

                return Ok($"{turma.Descricao} atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}");
            }


        }

        [HttpPost]
        public ActionResult<TurmaModel> PostResult(TurmaModel turma)
        {
            try
            {
                _turmaRepositorio.Adicionar(turma);
                _turmaRepositorio.SaveChanges();

                return CreatedAtAction("GetResult", new { id = turma.Id }, turma);


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

                var turma = _turmaRepositorio.ListarPorId(id);

                _turmaRepositorio.SaveChanges();

                return Ok($"{turma.Descricao} deletada.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}.");
            }
        }


    }
}
