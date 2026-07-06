using backend.Data;
using backend.DTOs.Totais;
using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class TotaisService(AppDbContext dbContext) : ITotaisService
{
    public async Task<TotaisResponse> ObterAsync(CancellationToken cancellationToken)
    {
        var pessoasComTransacoes = await dbContext.Pessoas
            .AsNoTracking()
            .Include(pessoa => pessoa.Transacoes)
            .OrderBy(pessoa => pessoa.Nome)
            .ToListAsync(cancellationToken);

        var pessoas = pessoasComTransacoes
            .Select(pessoa =>
            {
                var totalReceitas = pessoa.Transacoes
                    .Where(transacao => transacao.Tipo == TransacaoTipo.Receita)
                    .Sum(transacao => transacao.Valor);
                var totalDespesas = pessoa.Transacoes
                    .Where(transacao => transacao.Tipo == TransacaoTipo.Despesa)
                    .Sum(transacao => transacao.Valor);

                return new PessoaTotaisResponse(
                    pessoa.Id,
                    pessoa.Nome,
                    totalReceitas,
                    totalDespesas,
                    totalReceitas - totalDespesas);
            })
            .ToList();

        var totalReceitas = pessoas.Sum(pessoa => pessoa.TotalReceitas);
        var totalDespesas = pessoas.Sum(pessoa => pessoa.TotalDespesas);

        return new TotaisResponse(
            pessoas,
            new ResumoTotaisResponse(totalReceitas, totalDespesas, totalReceitas - totalDespesas));
    }
}
