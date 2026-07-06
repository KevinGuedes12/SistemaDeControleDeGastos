using System.ComponentModel.DataAnnotations;

namespace backend.Models.Entities;

public class Pessoa
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [Range(0, 150)]
    public int Idade { get; set; }

    public ICollection<Transacao> Transacoes { get; set; } = [];
}
