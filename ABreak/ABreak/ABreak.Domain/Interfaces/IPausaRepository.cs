using ABreak.Domain.Entities;

namespace ABreak.Domain.Interfaces
{
    public interface IPausaRepository : IRepository<Pausa>
    {
        Task<IEnumerable<Pausa>> GetPausasByUsuarioAsync(int usuarioId);
        Task<IEnumerable<Pausa>> GetPausasByUsuarioDataAsync(int usuarioId, DateTime data);
        Task<IEnumerable<Pausa>> GetPausasPaginadasAsync(int pageNumber, int pageSize);
    }
}