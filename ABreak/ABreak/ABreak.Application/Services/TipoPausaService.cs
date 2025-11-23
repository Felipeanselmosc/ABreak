using ABreak.Application.DTOs;
using ABreak.Application.Interfaces;
using ABreak.Domain.Entities;
using ABreak.Domain.Interfaces;

namespace ABreak.Application.Services
{
    public class TipoPausaService : ITipoPausaService
    {
        private readonly ITipoPausaRepository _tipoPausaRepository;

        public TipoPausaService(ITipoPausaRepository tipoPausaRepository)
        {
            _tipoPausaRepository = tipoPausaRepository;
        }

        public async Task<IEnumerable<TipoPausaDTO>> GetAllTiposPausaAsync()
        {
            var tipos = await _tipoPausaRepository.GetAllAsync();
            return tipos.Select(t => MapToDTO(t));
        }

        public async Task<TipoPausaDTO> GetTipoPausaByIdAsync(int id)
        {
            var tipo = await _tipoPausaRepository.GetByIdAsync(id);
            return tipo != null ? MapToDTO(tipo) : null;
        }

        public async Task<TipoPausaDTO> CreateTipoPausaAsync(TipoPausaDTO tipoPausaDto)
        {
            var tipoPausa = new TipoPausa
            {
                Nome = tipoPausaDto.Nome,
                Descricao = tipoPausaDto.Descricao,
                Icone = tipoPausaDto.Icone,
                DuracaoRecomendadaMinutos = tipoPausaDto.DuracaoRecomendadaMinutos,
                Ativo = true
            };

            var tipoCriado = await _tipoPausaRepository.AddAsync(tipoPausa);
            return MapToDTO(tipoCriado);
        }

        public async Task UpdateTipoPausaAsync(TipoPausaDTO tipoPausaDto)
        {
            var tipoPausa = await _tipoPausaRepository.GetByIdAsync(tipoPausaDto.Id);
            if (tipoPausa != null)
            {
                tipoPausa.Nome = tipoPausaDto.Nome;
                tipoPausa.Descricao = tipoPausaDto.Descricao;
                tipoPausa.Icone = tipoPausaDto.Icone;
                tipoPausa.DuracaoRecomendadaMinutos = tipoPausaDto.DuracaoRecomendadaMinutos;
                tipoPausa.Ativo = tipoPausaDto.Ativo;

                await _tipoPausaRepository.UpdateAsync(tipoPausa);
            }
        }

        public async Task DeleteTipoPausaAsync(int id)
        {
            await _tipoPausaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TipoPausaDTO>> GetTiposPausaAtivosAsync()
        {
            var tipos = await _tipoPausaRepository.GetTiposPausaAtivosAsync();
            return tipos.Select(t => MapToDTO(t));
        }

        private TipoPausaDTO MapToDTO(TipoPausa tipoPausa)
        {
            return new TipoPausaDTO
            {
                Id = tipoPausa.Id,
                Nome = tipoPausa.Nome,
                Descricao = tipoPausa.Descricao,
                Icone = tipoPausa.Icone,
                DuracaoRecomendadaMinutos = tipoPausa.DuracaoRecomendadaMinutos,
                Ativo = tipoPausa.Ativo
            };
        }
    }
}