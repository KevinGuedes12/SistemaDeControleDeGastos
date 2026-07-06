using backend.Data;
using backend.DTOs.Transacoes;
using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class TransacaoService(AppDbContext dbContext) : ITransacaoService
{
    public async Task<IReadOnlyList<TransacaoResponse>> ListarAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Transacoes
            .AsNoTracking()
            .Include(transacao => transacao.Pessoa)
            .OrderByDescending(transacao => transacao.Id)
            .Select(transacao => new TransacaoResponse(
                transacao.Id,
                transacao.Descricao,
                transacao.Valor,
                transacao.Tipo,
                transacao.PessoaId,
                transacao.Pessoa.Nome))
            .ToListAsync(cancellationToken);
    }

    public async Task<ServiceResult<TransacaoResponse>> CriarAsync(CreateTransacaoRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await dbContext.Pessoas.FindAsync([request.PessoaId], cancellationToken);
        if (pessoa is null)
        {
            return ServiceResult<TransacaoResponse>.Fail(
                ServiceErrorType.NotFound,
                "A pessoa informada para a transacao nao foi encontrada.");
        }

        // Regra de negocio critica: menores de idade podem registrar apenas despesas, nunca receitas.
        if (pessoa.Idade < 18 && request.Tipo == TransacaoTipo.Receita)
        {
            return ServiceResult<TransacaoResponse>.Fail(
                ServiceErrorType.BadRequest,
                "Pessoas menores de idade podem cadastrar somente transacoes do tipo Despesa.");
        }

        var transacao = new Transacao
        {
            Descricao = request.Descricao.Trim(),
            Valor = request.Valor,
            Tipo = request.Tipo,
            PessoaId = request.PessoaId
        };

        dbContext.Transacoes.Add(transacao);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ServiceResult<TransacaoResponse>.Ok(new TransacaoResponse(
            transacao.Id,
            transacao.Descricao,
            transacao.Valor,
            transacao.Tipo,
            transacao.PessoaId,
            pessoa.Nome));
    }
}
