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
        public ActionResult<IEnumerable<TurmaModel>> GetResult()
        {

            try
            {

                var turma = _turmaRepositorio.BuscarTodos();
                return Ok(turma);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Houve um erro: {ex.Message}.");

            }

        }

        [HttpGet("{id}")]
        public ActionResult<TurmaModel> GetResult(int id)
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
        public ActionResult PutResult(int id, TurmaModel turmaModel)
        {
            try
            {
                _turmaRepositorio.Atualizar(turmaModel);

                _turmaRepositorio.SaveChanges();

                var lstTurma = _turmaRepositorio.BuscarTodos();

                return Ok(lstTurma);
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

                var lstTurma = _turmaRepositorio.BuscarTodos();

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

                var turma = _turmaRepositorio.ListarPorId(id);

                _turmaRepositorio.SaveChanges();

                return Ok(turma);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}.");
            }
        }


    }



}

