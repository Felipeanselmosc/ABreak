namespace ABreak.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Pausa> Pausas { get; set; }
        public ICollection<ConfiguracaoPausa> Configuracoes { get; set; }
    }
}