using ABreak.Domain.Entities;

namespace ABreak.Domain.Interfaces
{
    public interface ITipoPausaRepository : IRepository<TipoPausa>
    {
        Task<IEnumerable<TipoPausa>> GetTiposPausaAtivosAsync();
    }
}