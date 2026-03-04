namespace RoyalGames.DTOs.JogoDto
{
    public class AtualizarJogoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco {  get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!;

        public int StatusJogoID { get; set; }

        public int ClassificacaoIndicativaID { get; set; }

        public List<int> GeneroIds { get; set; } = new();

        public List<int> PlataformaIds { get; set; } = new();
    }
}
