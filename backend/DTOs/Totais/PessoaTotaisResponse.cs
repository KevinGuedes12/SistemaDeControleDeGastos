namespace backend.DTOs.Totais;

public record PessoaTotaisResponse(
    Guid PessoaId,
    string Nome,
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo);
