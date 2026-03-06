namespace RoyalGames.DTOs.JogoDto
{
    public class LerJogoDto
    {
        public int JogoID { get; set; }

        public string? Nome { get; set; }

        public decimal? Preco {  get; set; }

        public byte[]? Imagem { get; set; }

        public string? Descricao { get; set; }

        // class indicativa
        public int? ClassificacaoIndicativaID { get; set; }

        public string ClassificacaoIndicativas { get; set; } = null!;

        // status
        public int? StatusJogoID { get; set; }

        public string StatusJogos { get; set; } = null!;

        // plataformas
        public List<int> PlataformaIds { get; set; } = new();
        public List<string> Plataformas { get; set; } = new();

        // generos
        public List<int> GeneroIds { get; set; } = new();
        public List<string> Generos { get; set; } = new();

        // usuario que cadastrou

        public int? UsuarioID { get; set; }
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }
    }
}
