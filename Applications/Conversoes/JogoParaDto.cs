using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;

namespace RoyalGames.Applications.Conversoes
{
    public class JogoParaDto
    {
        public static LerJogoDto ConverterParaDto(Jogo jogo)
        {
            return new LerJogoDto
            {
                JogoID = jogo.JogoID,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,

                StatusJogoID = jogo.StatusJogoID,
                StatusJogos = jogo.StatusJogo?.Disponibilidade ?? "vazio",

                ClassificacaoIndicativaID = jogo.ClassificacaoIndicativaID,
                ClassificacaoIndicativas = jogo.ClassificacaoIndicativa?.Classificacao ?? "vazio",

                PlataformaIds = jogo.Plataforma.Select(p => p.PlataformaID).ToList(),
                Plataformas = jogo.Plataforma.Select(p => p.Nome).ToList(),

                GeneroIds = jogo.Genero.Select(g => g.GeneroID).ToList(),
                Generos = jogo.Genero.Select(g => g.Gênero).ToList(),

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario?.Nome,
                UsuarioEmail = jogo.Usuario?.Email
            };
        }
    }
}
