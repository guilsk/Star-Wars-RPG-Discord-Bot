namespace DiscordRPGBot.Aplication.ModuloFichaPersonagem {
    public class Item {
        public string Nome { get; set; }
        public int Quantidade { get; set; }

        public Item() { }
        public Item(string nome) {
            Nome = nome;
            Quantidade = 1;
        }

        public Item(string nome, int quantidade) {
            Nome = nome;
            Quantidade = quantidade;
        }

    }
}
