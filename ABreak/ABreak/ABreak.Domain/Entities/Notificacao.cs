namespace ABreak.Domain.Entities
{
    public class Notificacao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int TipoPausaId { get; set; }
        public DateTime DataHoraNotificacao { get; set; }
        public bool Visualizada { get; set; }
        public bool PausaRealizada { get; set; }
        public DateTime? DataHoraVisualizacao { get; set; }
        public Usuario Usuario { get; set; }
        public TipoPausa TipoPausa { get; set; }
    }
}