using System.ComponentModel.DataAnnotations;
using backend.Models.Entities;

namespace backend.DTOs.Transacoes;

public class CreateTransacaoRequest : IValidatableObject
{
    [Required(ErrorMessage = "A descricao e obrigatoria.")]
    [MaxLength(180, ErrorMessage = "A descricao deve ter no maximo 180 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor e obrigatorio.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo e obrigatorio.")]
    public TransacaoTipo Tipo { get; set; }

    [Required(ErrorMessage = "A pessoa e obrigatoria.")]
    public Guid PessoaId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Valor <= 0)
        {
            yield return new ValidationResult("O valor deve ser positivo.", [nameof(Valor)]);
        }
    }
}
