using backend.Data;
using backend.DTOs.Pessoas;
using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class PessoaService(AppDbContext dbContext) : IPessoaService
{
    public async Task<IReadOnlyList<PessoaResponse>> ListarAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Pessoas
            .AsNoTracking()
            .OrderBy(pessoa => pessoa.Nome)
            .Select(pessoa => new PessoaResponse(pessoa.Id, pessoa.Nome, pessoa.Idade))
            .ToListAsync(cancellationToken);
    }

    public async Task<PessoaResponse> CriarAsync(CreatePessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoa = new Pessoa
        {
            Nome = request.Nome.Trim(),
            Idade = request.Idade
        };

        dbContext.Pessoas.Add(pessoa);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new PessoaResponse(pessoa.Id, pessoa.Nome, pessoa.Idade);
    }

    public async Task<bool> DeletarAsync(Guid id, CancellationToken cancellationToken)
    {
        var pessoa = await dbContext.Pessoas.FindAsync([id], cancellationToken);
        if (pessoa is null)
        {
            return false;
        }

        dbContext.Pessoas.Remove(pessoa);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
