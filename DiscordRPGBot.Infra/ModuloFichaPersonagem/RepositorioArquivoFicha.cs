using DiscordRPGBot.Domain.ModuloFichaPersonagem;
using DiscordRPGBot.Infra.Shared;

namespace DiscordRPGBot.Infra.ModuloFichaStarWars {
    public class RepositorioArquivoFicha
    {
        protected DataContext dataContext;

        public RepositorioArquivoFicha(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        protected List<FichaStarWars> ObterRegistros() {
            return dataContext.fichas;
        }

        public void Inserir(FichaStarWars novaFicha) {
            List<FichaStarWars> registros = ObterRegistros();

            registros.Add(novaFicha);
                
            dataContext.GravarEmArquivoJson();
        }

        public void Editar(FichaStarWars fichaAtualizada) {
            FichaStarWars ficha = SelecionarPorId(fichaAtualizada.Id);

            ficha.Editar(fichaAtualizada);

            dataContext.GravarEmArquivoJson();
        }

        public void Excluir(FichaStarWars fichaSelecionada) {
            List<FichaStarWars> fichas = ObterRegistros();

            fichas.Remove(fichaSelecionada);

            dataContext.GravarEmArquivoJson();
        }

        public FichaStarWars SelecionarPorId(Guid id) {
            List<FichaStarWars> registros = ObterRegistros();

            return registros.FirstOrDefault(x => x.Id == id);
        }

        public FichaStarWars SelecionarPorNome(string nome) {
            List<FichaStarWars> registros = ObterRegistros();

            return registros.FirstOrDefault(x => x.Nome == nome);
        }

        public List<FichaStarWars> SelecionarTodos() {
            return ObterRegistros();
        }

    }
}
