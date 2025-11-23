namespace ABreak.Application.DTOs
{
    public class TipoPausaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
        public int DuracaoRecomendadaMinutos { get; set; }
        public bool Ativo { get; set; }
    }
}