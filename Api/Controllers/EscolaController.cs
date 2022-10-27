using Biblioteca.Models;
using System.Collections.Generic;
using Biblioteca.Repositorio;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/escola")]
    [ApiController]

    public class EscolaController : ControllerBase
    {

        private readonly IEscolaRepositorio _escolaRepositorio;
        public EscolaController(IEscolaRepositorio escolaRepositorio)
        {
            _escolaRepositorio = escolaRepositorio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EscolaModel>> GetResult()
        {

            try
            {

               var escola = _escolaRepositorio.BuscarTodos();
                return Ok(escola);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Houve um erro: {ex.Message}.");

            }

        }

        [HttpGet("{id}")]
        public ActionResult<EscolaModel> GetResult(int id)
        {

            try
            {             
                var escola = _escolaRepositorio.ListarPorId(id);

                return Ok(escola);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutResult(int id,  EscolaModel escolaModel)
        {
            try
            {
                _escolaRepositorio.Atualizar(escolaModel);

                _escolaRepositorio.SaveChanges();

                var lstEscola = _escolaRepositorio.BuscarTodos();

                return Ok(lstEscola);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}");
            }


        }

        [HttpPost]
        public ActionResult<EscolaModel> PostResult(EscolaModel escola)
        {
            try
            {
                _escolaRepositorio.Adicionar(escola);
                _escolaRepositorio.SaveChanges();

                var lstEscola = _escolaRepositorio.BuscarTodos();

                return Ok(lstEscola);


            }
            catch (Exception ex)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro:{ex.Message}.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<EscolaModel> DeleteResult(int id)
        {

            try
            {
                _escolaRepositorio.Excluir(id);

                var escola = _escolaRepositorio.ListarPorId(id);

                _escolaRepositorio.SaveChanges();

                return Ok($"{escola.Nome} excluida com sucesso.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro: {ex.Message}.");
            }
        }


    }


}