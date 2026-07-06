using System.ComponentModel.DataAnnotations;

namespace backend.Models.Entities;

public class Transacao
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(180)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public decimal Valor { get; set; }

    [Required]
    public TransacaoTipo Tipo { get; set; }

    [Required]
    public Guid PessoaId { get; set; }

    public Pessoa Pessoa { get; set; } = null!;
}
