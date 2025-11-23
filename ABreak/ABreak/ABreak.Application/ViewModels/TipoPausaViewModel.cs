using System.ComponentModel.DataAnnotations;

namespace ABreak.Application.ViewModels
{
    public class TipoPausaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Nome { get; set; }

        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O ícone é obrigatório")]
        [StringLength(10)]
        [Display(Name = "Ícone")]
        public string Icone { get; set; }

        [Required(ErrorMessage = "A duração recomendada é obrigatória")]
        [Range(1, 60, ErrorMessage = "A duração deve ser entre 1 e 60 minutos")]
        [Display(Name = "Duração Recomendada (minutos)")]
        public int DuracaoRecomendadaMinutos { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    }
}