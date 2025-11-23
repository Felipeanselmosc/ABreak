using ABreak.Application.DTOs;

namespace ABreak.Application.Interfaces
{
    public interface ITipoPausaService
    {
        Task<IEnumerable<TipoPausaDTO>> GetAllTiposPausaAsync();
        Task<TipoPausaDTO> GetTipoPausaByIdAsync(int id);
        Task<TipoPausaDTO> CreateTipoPausaAsync(TipoPausaDTO tipoPausaDto);
        Task UpdateTipoPausaAsync(TipoPausaDTO tipoPausaDto);
        Task DeleteTipoPausaAsync(int id);
        Task<IEnumerable<TipoPausaDTO>> GetTiposPausaAtivosAsync();
    }
}