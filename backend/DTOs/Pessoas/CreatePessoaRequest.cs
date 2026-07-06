using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Pessoas;

public class CreatePessoaRequest
{
    [Required(ErrorMessage = "O nome e obrigatorio.")]
    [MaxLength(120, ErrorMessage = "O nome deve ter no maximo 120 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A idade e obrigatoria.")]
    [Range(0, 150, ErrorMessage = "A idade deve estar entre 0 e 150.")]
    public int Idade { get; set; }
}
