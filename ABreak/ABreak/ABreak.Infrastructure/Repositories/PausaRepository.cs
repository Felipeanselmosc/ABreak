using ABreak.Domain.Entities;
using ABreak.Domain.Interfaces;
using ABreak.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ABreak.Infrastructure.Repositories
{
    public class PausaRepository : Repository<Pausa>, IPausaRepository
    {
        public PausaRepository(ABreakDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pausa>> GetPausasByUsuarioAsync(int usuarioId)
        {
            return await _dbSet
                .Include(p => p.TipoPausa)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.DataHoraInicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pausa>> GetPausasByUsuarioDataAsync(int usuarioId, DateTime data)
        {
            return await _dbSet
                .Include(p => p.TipoPausa)
                .Where(p => p.UsuarioId == usuarioId && p.DataHoraInicio.Date == data.Date)
                .OrderByDescending(p => p.DataHoraInicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pausa>> GetPausasPaginadasAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .Include(p => p.Usuario)
                .Include(p => p.TipoPausa)
                .OrderByDescending(p => p.DataHoraInicio)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}