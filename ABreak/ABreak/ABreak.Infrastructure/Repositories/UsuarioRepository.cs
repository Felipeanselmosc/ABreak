using ABreak.Domain.Entities;
using ABreak.Domain.Interfaces;
using ABreak.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ABreak.Infrastructure.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ABreakDbContext context) : base(context)
        {
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAtivosAsync()
        {
            return await _dbSet.Where(u => u.Ativo).ToListAsync();
        }
    }
}