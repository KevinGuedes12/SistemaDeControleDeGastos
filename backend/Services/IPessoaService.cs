using backend.DTOs.Pessoas;

namespace backend.Services;

public interface IPessoaService
{
    Task<IReadOnlyList<PessoaResponse>> ListarAsync(CancellationToken cancellationToken);
    Task<PessoaResponse> CriarAsync(CreatePessoaRequest request, CancellationToken cancellationToken);
    Task<bool> DeletarAsync(Guid id, CancellationToken cancellationToken);
}
