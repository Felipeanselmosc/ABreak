namespace ABreak.Domain.Entities
{
    public class Pausa
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int TipoPausaId { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public int DuracaoMinutos { get; set; }
        public bool Completada { get; set; }
        public string? Observacao { get; set; }
        public Usuario Usuario { get; set; }
        public TipoPausa TipoPausa { get; set; }
    }
}