using RoyalGames.Domains;
using RoyalGames.DTOs.LogJogoDto;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class LogAlteracaoJogoService
    {
        private readonly ILogAlteracaoJogoRepository _repository;

        public LogAlteracaoJogoService(ILogAlteracaoJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogJogoDto> Listar()
        {
            List<Log_AlteracaoJogo> logs = _repository.Listar();

            List<LerLogJogoDto> listaLogJogos = logs.Select(log => new LerLogJogoDto
            {
                LogID = log.Log_AlteracaoJogoID,
                JogoID = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();

            return listaLogJogos;
        }

        public List<LerLogJogoDto> ListarPorJogo(int JogoId)
        {
            List<Log_AlteracaoJogo> logs = _repository.ListarPorJogo(JogoId);

            List<LerLogJogoDto> listaLogJogos = logs.Select(log => new LerLogJogoDto
            {
                LogID = log.Log_AlteracaoJogoID,
                JogoID = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();

            return listaLogJogos;
        }
    }
}
