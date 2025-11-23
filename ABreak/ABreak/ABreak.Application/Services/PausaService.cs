using ABreak.Application.DTOs;
using ABreak.Application.Interfaces;
using ABreak.Domain.Entities;
using ABreak.Domain.Interfaces;

namespace ABreak.Application.Services
{
    public class PausaService : IPausaService
    {
        private readonly IPausaRepository _pausaRepository;

        public PausaService(IPausaRepository pausaRepository)
        {
            _pausaRepository = pausaRepository;
        }

        public async Task<IEnumerable<PausaDTO>> GetAllPausasAsync()
        {
            var pausas = await _pausaRepository.GetAllAsync();
            return pausas.Select(p => MapToDTO(p));
        }

        public async Task<PausaDTO> GetPausaByIdAsync(int id)
        {
            var pausa = await _pausaRepository.GetByIdAsync(id);
            return pausa != null ? MapToDTO(pausa) : null;
        }

        public async Task<PausaDTO> CreatePausaAsync(PausaDTO pausaDto)
        {
            var pausa = new Pausa
            {
                UsuarioId = pausaDto.UsuarioId,
                TipoPausaId = pausaDto.TipoPausaId,
                DataHoraInicio = pausaDto.DataHoraInicio,
                DataHoraFim = pausaDto.DataHoraFim,
                DuracaoMinutos = pausaDto.DuracaoMinutos,
                Completada = pausaDto.Completada,
                Observacao = pausaDto.Observacao
            };

            var pausaCriada = await _pausaRepository.AddAsync(pausa);
            return MapToDTO(pausaCriada);
        }

        public async Task UpdatePausaAsync(PausaDTO pausaDto)
        {
            var pausa = await _pausaRepository.GetByIdAsync(pausaDto.Id);
            if (pausa != null)
            {
                pausa.TipoPausaId = pausaDto.TipoPausaId;
                pausa.DataHoraInicio = pausaDto.DataHoraInicio;
                pausa.DataHoraFim = pausaDto.DataHoraFim;
                pausa.DuracaoMinutos = pausaDto.DuracaoMinutos;
                pausa.Completada = pausaDto.Completada;
                pausa.Observacao = pausaDto.Observacao;

                await _pausaRepository.UpdateAsync(pausa);
            }
        }

        public async Task DeletePausaAsync(int id)
        {
            await _pausaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PausaDTO>> GetPausasByUsuarioAsync(int usuarioId)
        {
            var pausas = await _pausaRepository.GetPausasByUsuarioAsync(usuarioId);
            return pausas.Select(p => MapToDTO(p));
        }

        public async Task<IEnumerable<PausaDTO>> GetPausasPaginadasAsync(int pageNumber, int pageSize)
        {
            var pausas = await _pausaRepository.GetPausasPaginadasAsync(pageNumber, pageSize);
            return pausas.Select(p => MapToDTO(p));
        }

        private PausaDTO MapToDTO(Pausa pausa)
        {
            return new PausaDTO
            {
                Id = pausa.Id,
                UsuarioId = pausa.UsuarioId,
                UsuarioNome = pausa.Usuario?.Nome,
                TipoPausaId = pausa.TipoPausaId,
                TipoPausaNome = pausa.TipoPausa?.Nome,
                TipoPausaIcone = pausa.TipoPausa?.Icone,
                DataHoraInicio = pausa.DataHoraInicio,
                DataHoraFim = pausa.DataHoraFim,
                DuracaoMinutos = pausa.DuracaoMinutos,
                Completada = pausa.Completada,
                Observacao = pausa.Observacao
            };
        }
    }
}