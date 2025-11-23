using ABreak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ABreak.Infrastructure.Data
{
    public class ABreakDbContext : DbContext
    {
        public ABreakDbContext(DbContextOptions<ABreakDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoPausa> TiposPausa { get; set; }
        public DbSet<Pausa> Pausas { get; set; }
        public DbSet<ConfiguracaoPausa> ConfiguracoesPausa { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configuração da entidade TipoPausa
            modelBuilder.Entity<TipoPausa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descricao).HasMaxLength(200);
            });

            // Configuração da entidade Pausa
            modelBuilder.Entity<Pausa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Pausas)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.TipoPausa)
                      .WithMany(t => t.Pausas)
                      .HasForeignKey(e => e.TipoPausaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração da entidade ConfiguracaoPausa
            modelBuilder.Entity<ConfiguracaoPausa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Configuracoes)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.TipoPausa)
                      .WithMany(t => t.Configuracoes)
                      .HasForeignKey(e => e.TipoPausaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração da entidade Notificacao
            modelBuilder.Entity<Notificacao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.TipoPausa)
                      .WithMany()
                      .HasForeignKey(e => e.TipoPausaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed de dados iniciais (tipos de pausa padrão)
            modelBuilder.Entity<TipoPausa>().HasData(
                new TipoPausa { Id = 1, Nome = "Alongamento", Descricao = "Exercícios de alongamento", Icone = "🧘", DuracaoRecomendadaMinutos = 5, Ativo = true },
                new TipoPausa { Id = 2, Nome = "Hidratação", Descricao = "Beber água", Icone = "💧", DuracaoRecomendadaMinutos = 2, Ativo = true },
                new TipoPausa { Id = 3, Nome = "Descanso Visual", Descricao = "Descansar os olhos", Icone = "👁️", DuracaoRecomendadaMinutos = 3, Ativo = true },
                new TipoPausa { Id = 4, Nome = "Caminhada", Descricao = "Caminhar um pouco", Icone = "🚶", DuracaoRecomendadaMinutos = 10, Ativo = true }
            );
        }
    }
}