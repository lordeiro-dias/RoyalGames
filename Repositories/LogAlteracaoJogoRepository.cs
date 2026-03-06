using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class LogAlteracaoJogoRepository : ILogAlteracaoJogoRepository
    {
        private readonly Royal_GamesContext _context;

        public LogAlteracaoJogoRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Log_AlteracaoJogo> Listar()
        {
            List<Log_AlteracaoJogo> log = _context.Log_AlteracaoJogo.OrderByDescending(l => l.DataAlteracao).ToList();
            return log;
        }

        public List<Log_AlteracaoJogo> ListarPorJogo(int jogoId)
        {
            List<Log_AlteracaoJogo> alteracaoJogos = _context.Log_AlteracaoJogo
                .Where(log => log.JogoID == jogoId)
                .OrderByDescending(log => log.DataAlteracao).ToList();

            return alteracaoJogos;
        }
    }
}
