namespace ABreak.Application.DTOs
{
    public class ConfiguracaoPausaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int TipoPausaId { get; set; }
        public string TipoPausaNome { get; set; }
        public int IntervaloMinutos { get; set; }
        public TimeSpan HorarioInicioTrabalho { get; set; }
        public TimeSpan HorarioFimTrabalho { get; set; }
        public bool NotificacaoAtiva { get; set; }
        public bool Ativo { get; set; }
    }
}