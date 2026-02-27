using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class StatusJogo
{
    public int StatusJogoID { get; set; }

    public string? Disponibilidade { get; set; }

    public virtual ICollection<Jogo> Jogo { get; set; } = new List<Jogo>();

    public virtual ICollection<Log_AlteracaoJogo> Log_AlteracaoJogo { get; set; } = new List<Log_AlteracaoJogo>();
}
