using ABreak.Application.DTOs;

namespace ABreak.Application.Interfaces
{
    public interface IConfiguracaoPausaService
    {
        Task<IEnumerable<ConfiguracaoPausaDTO>> GetAllConfiguracoesAsync();
        Task<ConfiguracaoPausaDTO> GetConfiguracaoByIdAsync(int id);
        Task<ConfiguracaoPausaDTO> CreateConfiguracaoAsync(ConfiguracaoPausaDTO configuracaoDto);
        Task UpdateConfiguracaoAsync(ConfiguracaoPausaDTO configuracaoDto);
        Task DeleteConfiguracaoAsync(int id);
        Task<IEnumerable<ConfiguracaoPausaDTO>> GetConfiguracoesByUsuarioAsync(int usuarioId);
    }
}