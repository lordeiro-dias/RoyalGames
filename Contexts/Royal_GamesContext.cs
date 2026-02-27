using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RoyalGames.Domains;

namespace RoyalGames.Contexts;

public partial class Royal_GamesContext : DbContext
{
    public Royal_GamesContext()
    {
    }

    public Royal_GamesContext(DbContextOptions<Royal_GamesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassificacaoIndicativa> ClassificacaoIndicativa { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Jogo> Jogo { get; set; }

    public virtual DbSet<Log_AlteracaoJogo> Log_AlteracaoJogo { get; set; }

    public virtual DbSet<Plataforma> Plataforma { get; set; }

    public virtual DbSet<StatusJogo> StatusJogo { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Royal_Games;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassificacaoIndicativa>(entity =>
        {
            entity.HasKey(e => e.ClassificacaoIndicativaID).HasName("PK__Classifi__892DEC6F8643A2F9");

            entity.Property(e => e.Classificacao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroID).HasName("PK__Genero__A99D0268FF031295");

            entity.Property(e => e.Gênero)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.JogoID).HasName("PK__Jogo__5919685579FF04F6");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_AlteracaoProduto");
                    tb.HasTrigger("trg_ExclusaoProduto");
                });

            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.ClassificacaoIndicativa).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.ClassificacaoIndicativaID)
                .HasConstraintName("FK__Jogo__Classifica__5535A963");

            entity.HasOne(d => d.StatusJogo).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.StatusJogoID)
                .HasConstraintName("FK__Jogo__StatusJogo__5629CD9C");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.UsuarioID)
                .HasConstraintName("FK__Jogo__UsuarioID__571DF1D5");

            entity.HasMany(d => d.Genero).WithMany(p => p.Jogo)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroID")
                        .HasConstraintName("FK_JogoGenero_Genero"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("FK_JogoGenero_Jogo"),
                    j =>
                    {
                        j.HasKey("JogoID", "GeneroID");
                    });

            entity.HasMany(d => d.Plataforma).WithMany(p => p.Jogo)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoPlataforma",
                    r => r.HasOne<Plataforma>().WithMany()
                        .HasForeignKey("PlataformaID")
                        .HasConstraintName("FK_JogoPlataforma_Promocao"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("FK_JogoPlataforma_Jogo"),
                    j =>
                    {
                        j.HasKey("JogoID", "PlataformaID");
                    });
        });

        modelBuilder.Entity<Log_AlteracaoJogo>(entity =>
        {
            entity.HasKey(e => e.Log_AlteracaoJogoID).HasName("PK__Log_Alte__BB9D2C4F3CAD606A");

            entity.Property(e => e.DataAlteracao).HasPrecision(0);
            entity.Property(e => e.DisponibilidadeAnterior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecoAnterior).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Jogo).WithMany(p => p.Log_AlteracaoJogo)
                .HasForeignKey(d => d.JogoID)
                .HasConstraintName("FK__Log_Alter__JogoI__628FA481");

            entity.HasOne(d => d.StatusJogo).WithMany(p => p.Log_AlteracaoJogo)
                .HasForeignKey(d => d.StatusJogoID)
                .HasConstraintName("FK__Log_Alter__Statu__619B8048");
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.PlataformaID).HasName("PK__Platafor__B835678D73238A51");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusJogo>(entity =>
        {
            entity.HasKey(e => e.StatusJogoID).HasName("PK__StatusJo__4933E15F8EDB0AD7");

            entity.Property(e => e.Disponibilidade)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE7988EAFC732");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoUsuario"));

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuario).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
