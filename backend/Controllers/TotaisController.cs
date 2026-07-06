using backend.DTOs.Totais;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TotaisController(ITotaisService totaisService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(TotaisResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<TotaisResponse>> Obter(CancellationToken cancellationToken)
    {
        var totais = await totaisService.ObterAsync(cancellationToken);
        return Ok(totais);
    }
}
