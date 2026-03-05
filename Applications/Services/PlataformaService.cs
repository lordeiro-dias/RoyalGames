using RoyalGames.Domains;
using RoyalGames.DTOs.PlataformaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class PlataformaService
    {
        private readonly IPlataformaRepository _repository;

        public PlataformaService(IPlataformaRepository repository)
        {
            _repository = repository;
        }

        public List<LerPlataformaDto> Listar()
        {
            List<Plataforma> plataformas = _repository.Listar();

            List<LerPlataformaDto> plataformaDto = plataformas.Select(p => new LerPlataformaDto
            {
                PlataformaID = p.PlataformaID,
                Nome = p.Nome
            }).ToList();

            return plataformaDto;
        }

        public LerPlataformaDto ObterPorId(int id)
        {
            Plataforma plataforma = _repository.ObterPorId(id);

            if (plataforma == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }

            LerPlataformaDto plataformaDto = new LerPlataformaDto
            {
                PlataformaID = plataforma.PlataformaID,
                Nome = plataforma.Nome
            };

            return plataformaDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }

        public void Adicionar(CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Plataforma já existente.");
            }

            Plataforma plataforma = new Plataforma
            {
                Nome = criarDto.Nome
            };

            _repository.Adicionar(plataforma);
        }

        public void Atualizar(int id, CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if (plataformaBanco == null)
            {
                throw new DomainException("Plataforma não foi encontrada");
            }

            if (_repository.NomeExiste(criarDto.Nome, plataformaIdAtual: id))
            {
                throw new DomainException("Já existe outra plataforma com esse nome.");
            }

            plataformaBanco.Nome = criarDto.Nome;
            _repository.Atualizar(plataformaBanco);
        }

        public void Remover(int id)
        {
            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if (plataformaBanco == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }

            _repository.Remover(id);
        }
    }
}
