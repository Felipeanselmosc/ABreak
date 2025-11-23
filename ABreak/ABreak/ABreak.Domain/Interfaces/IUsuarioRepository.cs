using ABreak.Domain.Entities;

namespace ABreak.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetByEmailAsync(string email);
        Task<IEnumerable<Usuario>> GetUsuariosAtivosAsync();
    }
}