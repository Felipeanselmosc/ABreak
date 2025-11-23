using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 

namespace ABreak.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposPausa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Icone = table.Column<string>(type: "TEXT", nullable: false),
                    DuracaoRecomendadaMinutos = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPausa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracoesPausa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoPausaId = table.Column<int>(type: "INTEGER", nullable: false),
                    IntervaloMinutos = table.Column<int>(type: "INTEGER", nullable: false),
                    HorarioInicioTrabalho = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    HorarioFimTrabalho = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    NotificacaoAtiva = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesPausa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracoesPausa_TiposPausa_TipoPausaId",
                        column: x => x.TipoPausaId,
                        principalTable: "TiposPausa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfiguracoesPausa_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoPausaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataHoraNotificacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Visualizada = table.Column<bool>(type: "INTEGER", nullable: false),
                    PausaRealizada = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataHoraVisualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacoes_TiposPausa_TipoPausaId",
                        column: x => x.TipoPausaId,
                        principalTable: "TiposPausa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pausas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoPausaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataHoraFim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DuracaoMinutos = table.Column<int>(type: "INTEGER", nullable: false),
                    Completada = table.Column<bool>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pausas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pausas_TiposPausa_TipoPausaId",
                        column: x => x.TipoPausaId,
                        principalTable: "TiposPausa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pausas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TiposPausa",
                columns: new[] { "Id", "Ativo", "Descricao", "DuracaoRecomendadaMinutos", "Icone", "Nome" },
                values: new object[,]
                {
                    { 1, true, "Exercícios de alongamento", 5, "🧘", "Alongamento" },
                    { 2, true, "Beber água", 2, "💧", "Hidratação" },
                    { 3, true, "Descansar os olhos", 3, "👁️", "Descanso Visual" },
                    { 4, true, "Caminhar um pouco", 10, "🚶", "Caminhada" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesPausa_TipoPausaId",
                table: "ConfiguracoesPausa",
                column: "TipoPausaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesPausa_UsuarioId",
                table: "ConfiguracoesPausa",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_TipoPausaId",
                table: "Notificacoes",
                column: "TipoPausaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_UsuarioId",
                table: "Notificacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pausas_TipoPausaId",
                table: "Pausas",
                column: "TipoPausaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pausas_UsuarioId",
                table: "Pausas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracoesPausa");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "Pausas");

            migrationBuilder.DropTable(
                name: "TiposPausa");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
