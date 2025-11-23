namespace ABreak.Domain.Entities
{
    public class TipoPausa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
        public int DuracaoRecomendadaMinutos { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Pausa> Pausas { get; set; }
        public ICollection<ConfiguracaoPausa> Configuracoes { get; set; }
    }
}