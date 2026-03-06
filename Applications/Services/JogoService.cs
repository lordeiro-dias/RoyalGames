using RoyalGames.Applications.Conversoes;
using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();

            List<LerJogoDto> jogosDto = jogos.Select(JogoParaDto.ConverterParaDto).ToList();

            return jogosDto;
        }

        public LerJogoDto ObterPorId(int id)
        {
            Jogo jogo = _repository.ObterPorId(id);

            if(jogo == null)
            {
                throw new DomainException("Jogo não encontrado!");
            }

            return JogoParaDto.ConverterParaDto(jogo);
        }

        public static void ValidarCadastro(CriarJogoDto jogoDto)
        {
            if(string.IsNullOrEmpty(jogoDto.Nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }

            if(jogoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero.");
            }

            if(string.IsNullOrEmpty(jogoDto.Descricao))
            {
                throw new DomainException("Descrição é obrigatória");
            }

            if(jogoDto.Imagem == null || jogoDto.Imagem.Length == 0)
            {
                throw new DomainException("Imagem é obrigatória");
            }

            if(jogoDto.PlataformaIds == null || jogoDto.PlataformaIds.Count == 0)
            {
                throw new DomainException("Jogo deve ter ao menos uma plataforma.");
            }

            if(jogoDto.GeneroIds == null || jogoDto.GeneroIds.Count == 0)
            {
                throw new DomainException("Jogo deve ter ao menos um gênero.");
            }

            if(jogoDto.ClassificacaoIndicativaID == null)
            {
                throw new DomainException("Jogo deve possuir classificação indicativa.");
            }

            if(jogoDto.StatusJogoID == null)
            {
                throw new DomainException("Jogo deve possuir um status.");
            }
        }

        public byte[] ObterImagem(int id)
        {
            byte[] imagem = _repository.ObterImagem(id);

            if(imagem == null || imagem.Length == 0)
            {
                throw new DomainException("Imagem não encontrada");
            }

            return imagem;
        }

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int usuarioId)
        {
            ValidarCadastro(jogoDto);

            if(_repository.NomeExiste(jogoDto.Nome))
            {
                throw new DomainException("Jogo já existente");
            }

            Jogo jogo = new Jogo
            {
                Nome = jogoDto.Nome,
                Preco = jogoDto.Preco,
                Descricao = jogoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem),
                ClassificacaoIndicativaID = jogoDto.ClassificacaoIndicativaID,
                StatusJogoID = jogoDto.StatusJogoID,
                UsuarioID = usuarioId
            };

            _repository.Adicionar(jogo, jogoDto.GeneroIds, jogoDto.PlataformaIds);

            return JogoParaDto.ConverterParaDto(jogo);
        }

        public LerJogoDto Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            Jogo jogoBanco = _repository.ObterPorId(id);

            if(jogoBanco == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            if(_repository.NomeExiste(jogoBanco.Nome, jogoIdAtual: id))
            {
                throw new DomainException("Já existe outro jogo com este nome.");
            }

            if(jogoDto.GeneroIds == null || jogoDto.GeneroIds.Count == 0)
            {
                throw new DomainException("Jogo deve conter ao menos um gênero.");
            }

            if(jogoDto.PlataformaIds == null || jogoDto.PlataformaIds.Count == 0)
            {
                throw new DomainException("Jogo deve conter ao menos uma plataforma.");
            }

            if(jogoDto.StatusJogoID == null)
            {
                throw new DomainException("Jogo deve conter um Status.");
            }

            if(jogoDto.ClassificacaoIndicativaID == null)
            {
                throw new DomainException("Jogo deve conter uma Classificação Indicativa.");
            }

            jogoBanco.Nome = jogoDto.Nome;
            jogoBanco.Preco = jogoDto.Preco;
            jogoBanco.Descricao = jogoDto.Descricao;
            jogoBanco.ClassificacaoIndicativaID = jogoDto.ClassificacaoIndicativaID;
            jogoBanco.StatusJogoID = jogoDto.StatusJogoID;

            if(jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }

            _repository.Atualizar(jogoBanco, jogoDto.GeneroIds, jogoDto.PlataformaIds);

            return JogoParaDto.ConverterParaDto(jogoBanco);
        }

        public void Remover(int id)
        {
            Jogo jogo = _repository.ObterPorId(id);

            if(jogo == null)
            {
                throw new DomainException("Jogo não encontrado");
            }

            _repository.Remover(id);
        }
    }
}
