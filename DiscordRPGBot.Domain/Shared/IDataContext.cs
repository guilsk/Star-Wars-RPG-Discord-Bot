namespace DiscordRPGBot.Domain.Shared {
    public interface IDataContext {
        Task<bool> GravarAsync();
    }
}
