using backend.DTOs.Totais;

namespace backend.Services;

public interface ITotaisService
{
    Task<TotaisResponse> ObterAsync(CancellationToken cancellationToken);
}
