using System.ComponentModel.DataAnnotations;

namespace ABreak.Application.ViewModels
{
    public class PausaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione um usuário")]
        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Selecione um tipo de pausa")]
        [Display(Name = "Tipo de Pausa")]
        public int TipoPausaId { get; set; }

        [Required(ErrorMessage = "A data/hora de início é obrigatória")]
        [Display(Name = "Início")]
        public DateTime DataHoraInicio { get; set; }

        [Display(Name = "Fim")]
        public DateTime? DataHoraFim { get; set; }

        [Required(ErrorMessage = "A duração é obrigatória")]
        [Range(1, 120, ErrorMessage = "A duração deve ser entre 1 e 120 minutos")]
        [Display(Name = "Duração (minutos)")]
        public int DuracaoMinutos { get; set; }

        [Display(Name = "Completada")]
        public bool Completada { get; set; }

        [StringLength(500, ErrorMessage = "A observação deve ter no máximo 500 caracteres")]
        [Display(Name = "Observação")]
        public string? Observacao { get; set; }
    }
}