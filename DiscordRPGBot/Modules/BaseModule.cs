using Discord.Interactions;
using DiscordRPGBot.Aplication.ModuloFichaPersonagem;
using DiscordRPGBot.Infra.ModuloFichaStarWars;
using DiscordRPGBot.Infra.Shared;

namespace DiscordRPGBot.Modules {
    public abstract class BaseModule : InteractionModuleBase<SocketInteractionContext>{
        public static DataContext dataContext = new(true);
        public static RepositorioArquivoFicha repositorio = new RepositorioArquivoFicha(dataContext);
        public FichaStarWarsService fichaStarWarsService = new(repositorio, dataContext);
    }
}
