using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class Jogo
{
    public int JogoID { get; set; }

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public byte[]? Imagem { get; set; }

    public decimal? Preco { get; set; }

    public int? ClassificacaoIndicativaID { get; set; }

    public int? StatusJogoID { get; set; }

    public int? UsuarioID { get; set; }

    public virtual ClassificacaoIndicativa? ClassificacaoIndicativa { get; set; }

    public virtual ICollection<Log_AlteracaoJogo> Log_AlteracaoJogo { get; set; } = new List<Log_AlteracaoJogo>();

    public virtual StatusJogo? StatusJogo { get; set; }

    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<Genero> Genero { get; set; } = new List<Genero>();

    public virtual ICollection<Plataforma> Plataforma { get; set; } = new List<Plataforma>();
}
