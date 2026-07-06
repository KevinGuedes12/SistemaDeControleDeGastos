namespace backend.DTOs;

public record ApiErrorResponse(string Mensagem, IReadOnlyList<string>? Detalhes = null);
