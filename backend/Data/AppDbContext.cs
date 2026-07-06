using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Transacao> Transacoes => Set<Transacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(pessoa => pessoa.Id);
            entity.Property(pessoa => pessoa.Nome).IsRequired().HasMaxLength(120);
            entity.Property(pessoa => pessoa.Idade).IsRequired();
            entity.ToTable(table => table.HasCheckConstraint("CK_Pessoas_Idade_NaoNegativa", "Idade >= 0"));

            entity
                .HasMany(pessoa => pessoa.Transacoes)
                .WithOne(transacao => transacao.Pessoa)
                .HasForeignKey(transacao => transacao.PessoaId)
                // Garante que ao excluir uma pessoa todas as transacoes vinculadas sejam removidas pelo EF/banco.
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(transacao => transacao.Id);
            entity.Property(transacao => transacao.Descricao).IsRequired().HasMaxLength(180);
            entity.Property(transacao => transacao.Valor).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(transacao => transacao.Tipo).IsRequired();
            entity.ToTable(table => table.HasCheckConstraint("CK_Transacoes_Valor_Positivo", "Valor > 0"));
        });
    }
}
