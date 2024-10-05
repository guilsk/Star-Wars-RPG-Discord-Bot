using DiscordRPGBot.Aplication.ModuloFichaPersonagem;
using DiscordRPGBot.Domain.Shared;

namespace DiscordRPGBot.Domain.ModuloFichaPersonagem {
    public class FichaStarWars : Entidade {
        public string Nome { get; set; }
        public int Level { get; set; }
        public int AtkPerto { get; set; }
        public int AtkLonge { get; set; }
        public int Defesa { get; set; }
        public int Força { get; set; }
        public int Vida { get; set; }
        public int VidaAtual { get; set; }
        public int LadoNegro { get; set; }
        public List<Item> Itens { get; set; }

        public FichaStarWars() {}

        public FichaStarWars(string nome, int level, int atkPerto, int atkLonge, int defesa, int força, int vida, int ladoNegro) {
            Nome = nome;
            Level = level;
            AtkPerto = atkPerto;
            AtkLonge = atkLonge;
            Defesa = defesa;
            Força = força;
            Vida = vida;
            VidaAtual = vida;
            LadoNegro = ladoNegro;
            Itens = new List<Item>();
        }

        public void Editar(FichaStarWars novaFicha) {
            Nome = novaFicha.Nome;
            Level = novaFicha.Level;
            AtkPerto = novaFicha.AtkPerto;
            AtkLonge = novaFicha.AtkLonge;
            Defesa = novaFicha.Defesa;
            Força = novaFicha.Força;
            Vida = novaFicha.Vida;
            VidaAtual = novaFicha.Vida;
            LadoNegro = novaFicha.LadoNegro;
        }

        public override bool Equals(object? obj) {
            return obj is FichaStarWars wars &&
                   Id.Equals(wars.Id) &&
                   Nome == wars.Nome &&
                   Level == wars.Level &&
                   AtkPerto == wars.AtkPerto &&
                   AtkLonge == wars.AtkLonge &&
                   Defesa == wars.Defesa &&
                   Força == wars.Força &&
                   Vida == wars.Vida &&
                   VidaAtual == wars.VidaAtual &&
                   LadoNegro == wars.LadoNegro;
        }
    }
}
