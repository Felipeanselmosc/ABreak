using ABreak.Domain.Entities;

namespace ABreak.Domain.Interfaces
{
    public interface IConfiguracaoPausaRepository : IRepository<ConfiguracaoPausa>
    {
        Task<IEnumerable<ConfiguracaoPausa>> GetConfiguracoesByUsuarioAsync(int usuarioId);
    }
}