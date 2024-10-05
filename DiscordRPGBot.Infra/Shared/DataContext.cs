using DiscordRPGBot.Domain.ModuloFichaPersonagem;
using DiscordRPGBot.Domain.Shared;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordRPGBot.Infra.Shared {
    public class DataContext : IDataContext{
        private const string NOME_ARQUIVO = "ficha.json";

        public List<FichaStarWars> fichas;

        public DataContext() {
            fichas = new List<FichaStarWars>();
        }

        public DataContext(bool carregarDados) : this() {
            CarregarDadosDoArquivoJson();
        }

        public async Task<bool> GravarAsync() {
            GravarEmArquivoJson();
            return true;
        }

        public void GravarEmArquivoJson() {
            JsonSerializerOptions config = ObterConfiguracoes();

            string registrosJson = JsonSerializer.Serialize(this, config);

            File.WriteAllText(NOME_ARQUIVO, registrosJson);
        }

        private void CarregarDadosDoArquivoJson() {
            JsonSerializerOptions config = ObterConfiguracoes();

            if (File.Exists(NOME_ARQUIVO)) {
                string registrosJson = File.ReadAllText(NOME_ARQUIVO);

                if (registrosJson.Length > 0) {
                    DataContext ctx = JsonSerializer.Deserialize<DataContext>(registrosJson, config);

                    fichas = ctx.fichas;
                }
            }
        }

        private static JsonSerializerOptions ObterConfiguracoes() {
            JsonSerializerOptions opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.WriteIndented = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;

            return opcoes;
        }
    }
}
