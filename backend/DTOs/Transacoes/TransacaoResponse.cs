using backend.Models.Entities;

namespace backend.DTOs.Transacoes;

public record TransacaoResponse(
    Guid Id,
    string Descricao,
    decimal Valor,
    TransacaoTipo Tipo,
    Guid PessoaId,
    string PessoaNome);
