using backend.DTOs.Transacoes;

namespace backend.Services;

public interface ITransacaoService
{
    Task<IReadOnlyList<TransacaoResponse>> ListarAsync(CancellationToken cancellationToken);
    Task<ServiceResult<TransacaoResponse>> CriarAsync(CreateTransacaoRequest request, CancellationToken cancellationToken);
}
