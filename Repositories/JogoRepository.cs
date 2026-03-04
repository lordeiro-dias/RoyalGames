using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly Royal_GamesContext _context;

        public JogoRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
            List<Jogo> jogos = _context.Jogo
                .Include(jogo => jogo.Plataforma) // busca jogos, e para cada jogo, traz as suas plataformas
                .Include(jogo => jogo.Genero) // busca jogos, e para cada jogo, traz as seus generos
                .Include(jogo => jogo.ClassificacaoIndicativa) // busca jogos e para cada jogo, traz a sua class. indicativa
                .ToList();

            return jogos;
        }

        public Jogo ObterPorId(int id)
        {
            Jogo? jogo = _context.Jogo.Include(jogoDb => jogoDb.Plataforma).Include(jogoDb => jogoDb.Genero).Include(jogoDb => jogoDb.ClassificacaoIndicativa)
                .Include(jogoDb => jogoDb.StatusJogo)
                .FirstOrDefault(jogoDb => jogoDb.JogoID == id);

            return jogo;
        }

        public bool NomeExiste(string nome, int? jogoIdAtual = null)
        {
            // AsQueryable() -> Monta a consulta para executar passo a passo
            // monta a consulta na tabela jogo
            // não executa nada no banco ainda
            var jogoConsultado = _context.Jogo.AsQueryable();

            // Se o jogo atual tiver valor, então atualizamos o produto
            if (jogoIdAtual.HasValue)
            {
                jogoConsultado = jogoConsultado.Where(jogo => jogo.JogoID != jogoIdAtual.Value);
            }

            return jogoConsultado.Any(jogo => jogo.Nome == nome);
        }

        public byte[] ObterImagem(int id)
        {
            var jogo = _context.Jogo
                .Where(jogo => jogo.JogoID == id)
                .Select(jogo => jogo.Imagem)
                .FirstOrDefault();

            return jogo;
        }

        public void Adicionar(Jogo jogo, List<int> plataformaIds, List<int> generoIds)
        {
            List<Plataforma> plataformas = _context.Plataforma
                .Where(plataforma => plataformaIds.Contains(plataforma.PlataformaID))
                .ToList(); // Contains -> retorna true se houver o registro

            jogo.Plataforma = plataformas;

            List<Genero> generos = _context.Genero
                .Where(genero => generoIds.Contains(genero.GeneroID))
                .ToList();

            jogo.Genero = generos;

            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo, List<int> plataformaIds, List<int> generoIds)
        {
            Jogo? jogoBanco = _context.Jogo
                .Include(jogo => jogo.Plataforma)
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.StatusJogo)
                .Include(jogo => jogo.ClassificacaoIndicativa)
                .FirstOrDefault(jogoAux => jogoAux.JogoID == jogo.JogoID);

            if(jogoBanco == null)
            {
                return;
            }

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Descricao = jogo.Descricao;
            jogoBanco.ClassificacaoIndicativaID = jogo.ClassificacaoIndicativaID;
            jogoBanco.StatusJogoID = jogo.StatusJogoID;

            if(jogo.Imagem != null && jogo.Imagem.Length > 0)
            {
                jogoBanco.Imagem = jogo.Imagem;
            }

            // busca todas as categorias no banco com o id igual das categorias que vieram da requisição/front
            var plataformas = _context.Plataforma
                .Where(plataforma => plataformaIds.Contains(plataforma.PlataformaID))
                .ToList();
            // Clear() -> Remove as ligações atuais entre o produto e as categorias
            // ele não apaga a categoria do banco, só remove o vínculo com a tabela ProdutoCategoria
            jogoBanco.Plataforma.Clear();

            foreach(var plataforma in plataformas)
            {
                jogoBanco.Plataforma.Add(plataforma);
            }
            
            var generos = _context.Genero
                .Where(genero => generoIds.Contains(genero.GeneroID))
                .ToList();

            jogoBanco.Genero.Clear();

            foreach(var genero in generos)
            {
                jogoBanco.Genero.Add(genero);
            }

            _context.SaveChanges(); 

        }

        public void Remover(int id)
        {
            Jogo? jogo = _context.Jogo.FirstOrDefault(jogo => jogo.JogoID == id);

            if(jogo == null)
            {
                return;
            }

            _context.Jogo.Remove(jogo);
            _context.SaveChanges();
        }
    }
}
