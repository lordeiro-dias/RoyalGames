using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class Genero
{
    public int GeneroID { get; set; }

    public string? Gênero { get; set; }

    public virtual ICollection<Jogo> Jogo { get; set; } = new List<Jogo>();
}
