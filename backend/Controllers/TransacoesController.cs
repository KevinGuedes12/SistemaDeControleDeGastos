using backend.DTOs;
using backend.DTOs.Transacoes;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController(ITransacaoService transacaoService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TransacaoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransacaoResponse>>> Listar(CancellationToken cancellationToken)
    {
        var transacoes = await transacaoService.ListarAsync(cancellationToken);
        return Ok(transacoes);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TransacaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransacaoResponse>> Criar(CreateTransacaoRequest request, CancellationToken cancellationToken)
    {
        var resultado = await transacaoService.CriarAsync(request, cancellationToken);
        if (!resultado.Success)
        {
            var erro = new ApiErrorResponse(resultado.ErrorMessage ?? "Nao foi possivel criar a transacao.");
            return resultado.ErrorType == ServiceErrorType.NotFound ? NotFound(erro) : BadRequest(erro);
        }

        return Created($"/api/transacoes/{resultado.Data!.Id}", resultado.Data);
    }
}
