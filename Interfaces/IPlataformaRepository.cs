using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IPlataformaRepository
    {
        List<Plataforma> Listar();
        Plataforma ObterPorId(int id);
        bool NomeExiste(string nome, int? plataformaIdAtual = null);
        void Adicionar(Plataforma plataforma);
        void Atualizar(Plataforma plataforma);
        void Remover(int id);
    }
}
