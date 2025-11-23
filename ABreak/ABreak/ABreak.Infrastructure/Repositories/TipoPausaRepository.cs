using ABreak.Domain.Entities;
using ABreak.Domain.Interfaces;
using ABreak.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ABreak.Infrastructure.Repositories
{
    public class TipoPausaRepository : Repository<TipoPausa>, ITipoPausaRepository
    {
        public TipoPausaRepository(ABreakDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TipoPausa>> GetTiposPausaAtivosAsync()
        {
            return await _dbSet.Where(t => t.Ativo).ToListAsync();
        }
    }
}