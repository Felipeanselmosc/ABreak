using ABreak.Application.DTOs;
using ABreak.Application.Interfaces;
using ABreak.Domain.Entities;
using ABreak.Domain.Interfaces;

namespace ABreak.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                DataCadastro = u.DataCadastro,
                Ativo = u.Ativo
            });
        }

        public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo
            };
        }

        public async Task<UsuarioDTO> CreateUsuarioAsync(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                DataCadastro = DateTime.Now,
                Ativo = true
            };

            var usuarioCriado = await _usuarioRepository.AddAsync(usuario);

            return new UsuarioDTO
            {
                Id = usuarioCriado.Id,
                Nome = usuarioCriado.Nome,
                Email = usuarioCriado.Email,
                DataCadastro = usuarioCriado.DataCadastro,
                Ativo = usuarioCriado.Ativo
            };
        }

        public async Task UpdateUsuarioAsync(UsuarioDTO usuarioDto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioDto.Id);
            if (usuario != null)
            {
                usuario.Nome = usuarioDto.Nome;
                usuario.Email = usuarioDto.Email;
                usuario.Ativo = usuarioDto.Ativo;

                await _usuarioRepository.UpdateAsync(usuario);
            }
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<UsuarioDTO> GetUsuarioByEmailAsync(string email)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo
            };
        }
    }
}