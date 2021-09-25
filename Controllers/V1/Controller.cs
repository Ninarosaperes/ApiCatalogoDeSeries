using ApiCatalogoEmmyflix.Exceptions;
using ApiCatalogoEmmyflix.InputModel;
using ApiCatalogoEmmyflix.Services;
using ApiCatalogoEmmyflix.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoEmmyflix.Controllers.V1
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
        {
            private readonly ISerieService _serieService;

            public SeriesController(ISerieService serieService)
            {
            _serieService = serieService;
            }

            /// <summary>
            /// Buscar todas as séries por página.
            /// </summary>
            /// <remarks>
            /// Não é possível retornar as séries sem paginação.
            /// </remarks>
            /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1.</param>
            /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50.</param>
            /// <response code="200">Retorna a lista de series.</response>
            /// <response code="204">Caso não haja séries.</response>   
            [HttpGet]
            public async Task<ActionResult<IEnumerable<SerieViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
            {
                var series = await _serieService.Obter(pagina, quantidade);

                if (series.Count() == 0)
                    return NoContent();

                return Ok(series);
            }

            /// <summary>
            /// Buscar uma série pela sua Id
            /// </summary>
            /// <param name="idSerie">Id da serie buscada.</param>
            /// <response code="200">Retorna a série filtrada.</response>
            /// <response code="204">Caso não haja série com este id.</response>   
            [HttpGet("{idSerie:guid}")]
            public async Task<ActionResult<SerieViewModel>> Obter([FromRoute] Guid idSerie)
            {
                var serie = await _serieService.Obter(idSerie);

                if (serie == null)
                    return NoContent();

                return Ok(serie);
            }

            /// <summary>
            /// Inserir uma série no catálogo.
            /// </summary>
            /// <param name="serieInputModel">Dados da série a ser inserida.</param>
            /// <response code="200">Caso a série seja inserida com sucesso.</response>
            /// <response code="422">Caso já exista uma série com mesmo nome para o mesmo gênero.</response>   
            [HttpPost]
            public async Task<ActionResult<SerieViewModel>> InserirSerie([FromBody] SerieInputModel serieInputModel)
            {
                try
                {
                    var serie = await _serieService.Inserir(serieInputModel);

                    return Ok(serie);
                }
                catch (SerieJaCadastradaException ex)
                {
                    return UnprocessableEntity("Já existe uma série com este nome para este gênero.");
                }
            }

            /// <summary>
            /// Atualizar uma série no catálogo.
            /// </summary>
            /// /// <param name="idSerie">Id da série a ser atualizada.</param>
            /// <param name="serieInputModel">Novos dados para atualizar a série indicada.</param>
            /// <response code="200">Caso a série seja atualizada com sucesso.</response>
            /// <response code="404">Caso não exista uma série com este Id.</response>   
            [HttpPut("{idSerie:guid}")]
            public async Task<ActionResult> AtualizarSerie([FromRoute] Guid idSerie, [FromBody] SerieInputModel serieInputModel)
            {
                try
                {
                    await _serieService.Atualizar(idSerie, serieInputModel);

                    return Ok();
                }
                catch (SerieNaoCadastradaException ex)
                {
                    return NotFound("Não existe esta série no catálogo.");
                }
            }
           /// <summary>
           /// Atualizar a premiação de uma série.
           /// </summary>
           /// /// <param name="idSerie">Id da série a ser atualizada.</param>
           /// <param name="premio">Nova premiação da série.</param>
           /// <response code="200">Caso a premiação seja atualizado com sucesso.</response>
           /// <response code="404">Caso não exista uma série com este Id.</response>   
            [HttpPatch("{idSerie:guid}/premio/{premio:string}")]
            public async Task<ActionResult> AtualizarSerie([FromRoute] Guid idSerie, [FromRoute] string premio)
            {
                try
                {
                    await _serieService.Atualizar(idSerie, premio);

                    return Ok();
                }
                catch (SerieNaoCadastradaException ex)
                {
                    return NotFound("Não existe esta série no catálogo.");
                }
        }

            /// <summary>
            /// Atualizar o número de temporadas de uma série.
            /// </summary>
            /// /// <param name="idSerie">Id da série a ser atualizada.</param>
            /// <param name="numeroTemporadas">Novo número de temporadas da série.</param>
            /// <response code="200">Caso o numero de temporadas seja atualizado com sucesso.</response>
            /// <response code="404">Caso não exista uma série com este Id.</response>   
            [HttpPatch("{idSerie:guid}/numeroTemporadas/{numeroTemporadas:int}")]
            public async Task<ActionResult> AtualizarSerie([FromRoute] Guid idSerie, [FromRoute] int numeroTemporadas)
            {
                try
                {
                    await _serieService.Atualizar(idSerie, numeroTemporadas);

                    return Ok();
                }
                catch (SerieNaoCadastradaException ex)
                {
                    return NotFound("Não existe esta série no catálogo.");
                }
            }

            /// <summary>
            /// Excluir uma série.
            /// </summary>
            /// /// <param name="idSerie">Id da serie a ser excluída.</param>
            /// <response code="200">Caso a série seja excluída com sucesso.</response>
            /// <response code="404">Caso não exista uma série com este Id.</response>   
            [HttpDelete("{idSerie:guid}")]
            public async Task<ActionResult> ApagarSerie([FromRoute] Guid idSerie)
            {
                try
                {
                    await _serieService.Remover(idSerie);

                    return Ok();
                }
                catch (SerieNaoCadastradaException ex)
                {
                    return NotFound("Não existe esta série no catálogo.");
                }
            }
        }
}
