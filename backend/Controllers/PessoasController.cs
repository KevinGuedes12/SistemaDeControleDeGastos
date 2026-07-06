using backend.DTOs;
using backend.DTOs.Pessoas;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController(IPessoaService pessoaService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PessoaResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PessoaResponse>>> Listar(CancellationToken cancellationToken)
    {
        var pessoas = await pessoaService.ListarAsync(cancellationToken);
        return Ok(pessoas);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PessoaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PessoaResponse>> Criar(CreatePessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await pessoaService.CriarAsync(request, cancellationToken);
        return Created($"/api/pessoas/{pessoa.Id}", pessoa);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        var deletou = await pessoaService.DeletarAsync(id, cancellationToken);
        if (!deletou)
        {
            return NotFound(new ApiErrorResponse("Pessoa nao encontrada."));
        }

        return NoContent();
    }
}
