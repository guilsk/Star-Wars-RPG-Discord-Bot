namespace DiscordRPGBot.Domain.Shared {
    public abstract class Entidade {
        public Guid Id { get; set; }

        public Entidade() {
            Id = Guid.NewGuid();
        }
    }
}
