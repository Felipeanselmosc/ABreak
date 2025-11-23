namespace ABreak.Domain.Entities
{
    public class ConfiguracaoPausa
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int TipoPausaId { get; set; }
        public int IntervaloMinutos { get; set; } 
        public TimeSpan HorarioInicioTrabalho { get; set; }
        public TimeSpan HorarioFimTrabalho { get; set; }
        public bool NotificacaoAtiva { get; set; }
        public bool Ativo { get; set; }
         
        public Usuario Usuario { get; set; }
        public TipoPausa TipoPausa { get; set; }
    }
}