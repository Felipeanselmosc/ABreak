namespace ABreak.Application.DTOs
{
    public class PausaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public int TipoPausaId { get; set; }
        public string TipoPausaNome { get; set; }
        public string TipoPausaIcone { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public int DuracaoMinutos { get; set; }
        public bool Completada { get; set; }
        public string? Observacao { get; set; }
    }
}