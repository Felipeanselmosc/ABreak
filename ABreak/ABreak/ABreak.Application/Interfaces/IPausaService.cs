using ABreak.Application.DTOs;

namespace ABreak.Application.Interfaces
{
    public interface IPausaService
    {
        Task<IEnumerable<PausaDTO>> GetAllPausasAsync();
        Task<PausaDTO> GetPausaByIdAsync(int id);
        Task<PausaDTO> CreatePausaAsync(PausaDTO pausaDto);
        Task UpdatePausaAsync(PausaDTO pausaDto);
        Task DeletePausaAsync(int id);
        Task<IEnumerable<PausaDTO>> GetPausasByUsuarioAsync(int usuarioId);
        Task<IEnumerable<PausaDTO>> GetPausasPaginadasAsync(int pageNumber, int pageSize);
    }
}