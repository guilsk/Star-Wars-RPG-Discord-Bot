using DiscordRPGBot.Domain.ModuloFichaPersonagem;
using DiscordRPGBot.Infra.ModuloFichaStarWars;
using DiscordRPGBot.Infra.Shared;
using FluentResults;

namespace DiscordRPGBot.Aplication.ModuloFichaPersonagem {
    public class FichaStarWarsService {
        private readonly RepositorioArquivoFicha repositorio;
        private readonly DataContext dataContext;

        public FichaStarWarsService(RepositorioArquivoFicha repositorio, DataContext dataContext) {
            this.repositorio = repositorio;
            this.dataContext = dataContext;
        }

        public async Task<Result<List<FichaStarWars>>> SelecionarTodosAsync() {
            var ficha = repositorio.SelecionarTodos();

            return Result.Ok(ficha);
        }

        public async Task<Result<FichaStarWars>> SelecionarPorNomeAsync(string nome) {
            var ficha = repositorio.SelecionarPorNome(nome);

            return Result.Ok(ficha);
        }

        public async Task<Result<FichaStarWars>> InserirAsync(FichaStarWars ficha) {
            if (ValidarNome(ficha)) return Result.Fail("Esse nome de personagem já está em uso.");

            repositorio.Inserir(ficha);

            await dataContext.GravarAsync();

            return Result.Ok();
        }

        public async Task<Result<FichaStarWars>> EditarAsync(FichaStarWars ficha) {
            if (ValidarNomeEdicao(ficha)) return Result.Fail("Esse nome de personagem já está em uso.");

            repositorio.Editar(ficha);

            await dataContext.GravarAsync();

            return Result.Ok();
        }

        public async Task<Result<FichaStarWars>> ExcluirAsync(FichaStarWars ficha) {
            repositorio.Excluir(ficha);

            await dataContext.GravarAsync();

            return Result.Ok();
        }
        private bool ValidarNome(FichaStarWars ficha) {
            foreach (var item in repositorio.SelecionarTodos()) {
                if (ficha.Nome == item.Nome) return true;
            }
            return false;
        }

        private bool ValidarNomeEdicao(FichaStarWars ficha) {
            foreach (var item in repositorio.SelecionarTodos()) {
                if (ficha.Nome == item.Nome && !ficha.Equals(item)) return true;
            }
            return false;
        }
    }
}
