namespace backend.DTOs.Totais;

public record TotaisResponse(IReadOnlyList<PessoaTotaisResponse> Pessoas, ResumoTotaisResponse Geral);
