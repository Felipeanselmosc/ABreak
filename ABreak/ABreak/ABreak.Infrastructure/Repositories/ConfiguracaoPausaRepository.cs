using ABreak.Domain.Entities;
using ABreak.Domain.Interfaces;
using ABreak.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ABreak.Infrastructure.Repositories
{
    public class ConfiguracaoPausaRepository : Repository<ConfiguracaoPausa>, IConfiguracaoPausaRepository
    {
        public ConfiguracaoPausaRepository(ABreakDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ConfiguracaoPausa>> GetConfiguracoesByUsuarioAsync(int usuarioId)
        {
            return await _dbSet
                .Include(c => c.TipoPausa)
                .Where(c => c.UsuarioId == usuarioId && c.Ativo)
                .ToListAsync();
        }
    }
}