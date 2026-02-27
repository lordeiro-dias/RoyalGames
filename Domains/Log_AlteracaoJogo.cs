using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class Log_AlteracaoJogo
{
    public int Log_AlteracaoJogoID { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string? NomeAnterior { get; set; }

    public decimal? PrecoAnterior { get; set; }

    public string? DisponibilidadeAnterior { get; set; }

    public int? StatusJogoID { get; set; }

    public int? JogoID { get; set; }

    public virtual Jogo? Jogo { get; set; }

    public virtual StatusJogo? StatusJogo { get; set; }
}
