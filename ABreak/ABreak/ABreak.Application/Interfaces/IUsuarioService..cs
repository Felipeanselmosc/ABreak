using ABreak.Application.DTOs;

namespace ABreak.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
        Task<UsuarioDTO> GetUsuarioByIdAsync(int id);
        Task<UsuarioDTO> CreateUsuarioAsync(UsuarioDTO usuarioDto);
        Task UpdateUsuarioAsync(UsuarioDTO usuarioDto);
        Task DeleteUsuarioAsync(int id);
        Task<UsuarioDTO> GetUsuarioByEmailAsync(string email);
    }
}