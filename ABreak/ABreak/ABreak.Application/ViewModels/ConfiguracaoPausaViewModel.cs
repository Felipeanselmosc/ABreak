using System.ComponentModel.DataAnnotations;

namespace ABreak.Application.ViewModels
{
    public class ConfiguracaoPausaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione um usuário")]
        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Selecione um tipo de pausa")]
        [Display(Name = "Tipo de Pausa")]
        public int TipoPausaId { get; set; }

        [Required(ErrorMessage = "O intervalo é obrigatório")]
        [Range(15, 480, ErrorMessage = "O intervalo deve ser entre 15 e 480 minutos")]
        [Display(Name = "Intervalo (minutos)")]
        public int IntervaloMinutos { get; set; }

        [Required(ErrorMessage = "O horário de início é obrigatório")]
        [Display(Name = "Horário Início Trabalho")]
        public TimeSpan HorarioInicioTrabalho { get; set; }

        [Required(ErrorMessage = "O horário de fim é obrigatório")]
        [Display(Name = "Horário Fim Trabalho")]
        public TimeSpan HorarioFimTrabalho { get; set; }

        [Display(Name = "Notificação Ativa")]
        public bool NotificacaoAtiva { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    }
}
