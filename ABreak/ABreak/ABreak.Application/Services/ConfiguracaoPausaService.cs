using ABreak.Application.DTOs;
using ABreak.Application.Interfaces;
using ABreak.Domain.Entities;
using ABreak.Domain.Interfaces;

namespace ABreak.Application.Services
{
    public class ConfiguracaoPausaService : IConfiguracaoPausaService
    {
        private readonly IConfiguracaoPausaRepository _configuracaoRepository;

        public ConfiguracaoPausaService(IConfiguracaoPausaRepository configuracaoRepository)
        {
            _configuracaoRepository = configuracaoRepository;
        }

        public async Task<IEnumerable<ConfiguracaoPausaDTO>> GetAllConfiguracoesAsync()
        {
            var configuracoes = await _configuracaoRepository.GetAllAsync();
            return configuracoes.Select(c => MapToDTO(c));
        }

        public async Task<ConfiguracaoPausaDTO> GetConfiguracaoByIdAsync(int id)
        {
            var configuracao = await _configuracaoRepository.GetByIdAsync(id);
            return configuracao != null ? MapToDTO(configuracao) : null;
        }

        public async Task<ConfiguracaoPausaDTO> CreateConfiguracaoAsync(ConfiguracaoPausaDTO configuracaoDto)
        {
            var configuracao = new ConfiguracaoPausa
            {
                UsuarioId = configuracaoDto.UsuarioId,
                TipoPausaId = configuracaoDto.TipoPausaId,
                IntervaloMinutos = configuracaoDto.IntervaloMinutos,
                HorarioInicioTrabalho = configuracaoDto.HorarioInicioTrabalho,
                HorarioFimTrabalho = configuracaoDto.HorarioFimTrabalho,
                NotificacaoAtiva = configuracaoDto.NotificacaoAtiva,
                Ativo = true
            };

            var configuracaoCriada = await _configuracaoRepository.AddAsync(configuracao);
            return MapToDTO(configuracaoCriada);
        }

        public async Task UpdateConfiguracaoAsync(ConfiguracaoPausaDTO configuracaoDto)
        {
            var configuracao = await _configuracaoRepository.GetByIdAsync(configuracaoDto.Id);
            if (configuracao != null)
            {
                configuracao.TipoPausaId = configuracaoDto.TipoPausaId;
                configuracao.IntervaloMinutos = configuracaoDto.IntervaloMinutos;
                configuracao.HorarioInicioTrabalho = configuracaoDto.HorarioInicioTrabalho;
                configuracao.HorarioFimTrabalho = configuracaoDto.HorarioFimTrabalho;
                configuracao.NotificacaoAtiva = configuracaoDto.NotificacaoAtiva;
                configuracao.Ativo = configuracaoDto.Ativo;

                await _configuracaoRepository.UpdateAsync(configuracao);
            }
        }

        public async Task DeleteConfiguracaoAsync(int id)
        {
            await _configuracaoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ConfiguracaoPausaDTO>> GetConfiguracoesByUsuarioAsync(int usuarioId)
        {
            var configuracoes = await _configuracaoRepository.GetConfiguracoesByUsuarioAsync(usuarioId);
            return configuracoes.Select(c => MapToDTO(c));
        }

        private ConfiguracaoPausaDTO MapToDTO(ConfiguracaoPausa configuracao)
        {
            return new ConfiguracaoPausaDTO
            {
                Id = configuracao.Id,
                UsuarioId = configuracao.UsuarioId,
                TipoPausaId = configuracao.TipoPausaId,
                TipoPausaNome = configuracao.TipoPausa?.Nome,
                IntervaloMinutos = configuracao.IntervaloMinutos,
                HorarioInicioTrabalho = configuracao.HorarioInicioTrabalho,
                HorarioFimTrabalho = configuracao.HorarioFimTrabalho,
                NotificacaoAtiva = configuracao.NotificacaoAtiva,
                Ativo = configuracao.Ativo
            };
        }
    }
}