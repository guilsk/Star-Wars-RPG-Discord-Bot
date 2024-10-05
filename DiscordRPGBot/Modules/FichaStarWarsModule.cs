using Discord.Interactions;
using DiscordRPGBot.Aplication.ModuloFichaPersonagem;
using DiscordRPGBot.Domain.ModuloFichaPersonagem;

namespace DiscordRPGBot.Modules {
    public class FichaStarWarsModule : BaseModule {

        [SlashCommand("criar", "Distribua os pontos na seguinte ordem: Nome Level AtkPerto AtkLonge Defesa Força Vida")]
        public async Task Criar(string nome, int level, int atkPerto, int atkLonge, int defesa, int forca, int vida) {
            FichaStarWars ficha = new FichaStarWars(nome, level, atkPerto, atkLonge, defesa, forca, vida, 0);

            var fichaResult = await fichaStarWarsService.InserirAsync(ficha);
            if(fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Já existe um personagem com este nome: {ficha.Nome}");
            } else {
                await RespondAsync($"Ficha do **{ficha.Nome}** criada com sucesso!");
            }
        }

        [SlashCommand("ficha", "Informe o nome do personagem")]
        public async Task Ficha(string nome) {
            var fichaResult = await fichaStarWarsService.SelecionarPorNomeAsync(nome);

            if(fichaResult.IsFailed ) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Não existe um personagem com este nome!");
            } else {
                FichaStarWars ficha = repositorio.SelecionarPorNome(nome);
                await RespondAsync( $"\n**{ficha.Nome} - Level {ficha.Level}**" +
                                    $"\n- Ataque de Perto: {ficha.AtkPerto}" +
                                    $"\n- Ataque de Longe: {ficha.AtkLonge}" +
                                    $"\n- Defesa: {ficha.Defesa}" +
                                    $"\n- Força: {ficha.Força}" +
                                    $"\n- Vida: {ficha.VidaAtual}/{ficha.Vida}" +
                                    $"\n- Lado Negro: {ficha.LadoNegro}");
            }
        }

        [SlashCommand("dano", "Causa dano a um personagem")]
        public async Task Dano(string nome, int dano) {
            var fichaResult = await fichaStarWarsService.SelecionarPorNomeAsync(nome);

            if(fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Não existe um personagem com este nome!");
            } else {
                FichaStarWars ficha = repositorio.SelecionarPorNome(nome);

                if (dano >= ficha.VidaAtual) {
                    ficha.VidaAtual = 0;
                } else {
                    ficha.VidaAtual -= dano;
                }

                await RespondAsync($"**{ficha.Nome}** recebeu **{dano}** de dano. Vida: {ficha.VidaAtual}/{ficha.Vida}");
            }
        }

        [SlashCommand("cura", "Cura um personagem")]
        public async Task Cura(string nome, int cura) {
            var fichaResult = await fichaStarWarsService.SelecionarPorNomeAsync(nome);

            if (fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Não existe um personagem com este nome!");
            } else {
                FichaStarWars ficha = repositorio.SelecionarPorNome(nome);

                if (ficha.VidaAtual + cura >= ficha.Vida) {
                    ficha.VidaAtual = ficha.Vida;
                } else {
                    ficha.VidaAtual += cura;
                }

                await RespondAsync($"**{ficha.Nome}** curou **{cura}** de vida. Vida: {ficha.VidaAtual}/{ficha.Vida}");
            }
        }

        [SlashCommand("lvlup", "Informe o nome do personagem e o atributo a ser upado")]
        public async Task LvlUp(string nome, string atributo) {
            var fichaResult = await fichaStarWarsService.SelecionarPorNomeAsync(nome);

            if(fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Não existe um personagem com este nome!");
            } else {
                FichaStarWars ficha = repositorio.SelecionarPorNome(nome);
                bool atributoValido = atributo == "perto" || atributo == "longe" || atributo == "defesa" || atributo == "forca" || atributo == "vida"
                                   || atributo == "p" || atributo == "l" || atributo == "d" || atributo == "f" || atributo == "v";
                if (atributoValido) {
                    ficha.Level++;
                    if (atributo == "perto" || atributo == "p") ficha.AtkPerto++;
                    if (atributo == "longe" || atributo == "l") ficha.AtkLonge++;
                    if (atributo == "defesa" || atributo == "d") ficha.Defesa++;
                    if (atributo == "forca" || atributo == "f") ficha.Força++;
                    if (atributo == "vida" || atributo == "v") ficha.Vida++;
                    repositorio.Editar(ficha);
                    await RespondAsync($"{ficha.Nome} subiu de Lvl aumentando seu status {atributo} em 1 ponto!");
                } else {
                    await RespondAsync( "Atributo inválido! Tente o seguinte:" +
                                        "\n`perto` ou `p` para Ataque de Perto" +
                                        "\n`longe` ou `l` para Ataque de Longe" +
                                        "\n`defesa` ou `d` para Defesa" +
                                        "\n`forca` ou `f` para Força" +
                                        "\n`vida` ou `v` para Vida");
                }
            }
        }

        [SlashCommand("all", "Mostra todos os personagens")]
        public async Task All() {
            var fichaResult = await fichaStarWarsService.SelecionarTodosAsync();

            if(fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync("Parece que não há nenhum personagem salvo...");
            } else {
                List<FichaStarWars> fichas = repositorio.SelecionarTodos();
                string resposta = "Personagens:";
                for (int i = 0; i < fichas.Count(); i++) {
                    resposta += $"\n{i + 1}. **{fichas[i].Nome}** - Lvl {fichas[i].Level} | Vida: {fichas[i].VidaAtual}/{fichas[i].Vida}";
                }
                await RespondAsync(resposta);
            }
        }

        [SlashCommand("editar", "Insira o nome do personagem e seus novos valores")]
        public async Task Editar(string nome, string novoNome, int level, int atkPerto, int atkLonge, int defesa, int forca, int vida) {
            var fichaResult = await fichaStarWarsService.SelecionarPorNomeAsync(nome);
            if (fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Não existe um personagem com este nome!");
            } else {
                FichaStarWars ficha = repositorio.SelecionarPorNome(nome);
                ficha = new FichaStarWars(novoNome, level, atkPerto, atkLonge, defesa, forca, vida, 0);

                fichaResult = await fichaStarWarsService.EditarAsync(ficha);
                if (fichaResult.IsFailed) {
                    Console.WriteLine(fichaResult.Errors[0]);
                    await RespondAsync($"Já existe um personagem com este nome: {ficha.Nome}");
                } else {
                    await RespondAsync($"Ficha de **{ficha.Nome}** editada com sucesso!");
                }
            }
        }

        [SlashCommand("excluir", "Exclui um personagem")]
        public async Task Excluir(string nome) {
            FichaStarWars ficha = repositorio.SelecionarPorNome(nome);
            var fichaResult = await fichaStarWarsService.ExcluirAsync(ficha);

            if(fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Não existe um personagem com este nome!");
            } else {
                await RespondAsync($"Ficha de **{ficha.Nome}** excluída com sucesso!");
            }
        }

        [SlashCommand("inventario", "Mostra o inventário do personagem")]
        public async Task Inventario(string nome) {
            var fichaResult = await fichaStarWarsService.SelecionarPorNomeAsync(nome);

            if(fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Não existe um personagem com este nome!");
            } else {
                FichaStarWars ficha = repositorio.SelecionarPorNome(nome);

                if (ficha.Itens.Any()) {

                    string resposta = $"**Inventário {ficha.Nome}**";

                    foreach (var item in ficha.Itens) {
                        resposta += $"\n- {item.Nome} x{item.Quantidade}";
                    }

                    await RespondAsync(resposta);

                } else {
                    await RespondAsync($"{ficha.Nome} não possui itens...");
                }
            }
        }

        [SlashCommand("additem", "Adicione um item")]
        public async Task AddItem(string nomePersonagem, string nomeItem, int quantidade) {
            var fichaResult = await fichaStarWarsService.SelecionarPorNomeAsync(nomePersonagem);

            if(fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Não existe um personagem com este nome!");
            } else {
                FichaStarWars ficha = repositorio.SelecionarPorNome(nomePersonagem);

                if(ficha.Itens != null) {
                    Item item = new Item();
                    foreach (var i in ficha.Itens) {
                        if (i.Nome == nomeItem) {
                            item = i;
                        }
                    }

                    if (item.Quantidade > 0) {
                        item.Quantidade += quantidade;
                    } else {
                        ficha.Itens.Add(new Item(nomeItem, quantidade));
                    }
                } else {
                    ficha.Itens = new List<Item> {
                        new Item(nomeItem, quantidade)
                    };
                }

                repositorio.Editar(ficha);

                await RespondAsync($"Item {nomeItem} adicionado com sucesso!");
            }
        }

        [SlashCommand("removeitem", "Adicione um item")]
        public async Task RemoveItem(string nomePersonagem, string nomeItem, int quantidade) {
            var fichaResult = await fichaStarWarsService.SelecionarPorNomeAsync(nomePersonagem);

            if (fichaResult.IsFailed) {
                Console.WriteLine(fichaResult.Errors[0]);
                await RespondAsync($"Não existe um personagem com este nome!");
            } else {
                FichaStarWars ficha = repositorio.SelecionarPorNome(nomePersonagem);
                Item item = new Item();
                foreach (var i in ficha.Itens) {
                    if (i.Nome == nomeItem) {
                        item = i;
                    }
                }

                if(item != null) {
                    if (quantidade >= item.Quantidade) {
                        ficha.Itens.Remove(item);

                    } else {
                        item.Quantidade -= quantidade;

                    }
                }

                repositorio.Editar(ficha);

                await RespondAsync($"Item {nomeItem} removido com sucesso!");
            }
        }

        [SlashCommand("help", "Mostra todos os comandos")]
        public async Task Help() {
            await RespondAsync( $"- /criar [nome] [level] [atkPerto] [atkLonge] [defesa] [forca] [vida]" +
                                $"\n\tCria um personagem com esses status. O personagem será criado sem itens e com Lado Negro 0" +
                                $"\n- /ficha [nome]" +
                                $"\n\tMostra a ficha de um personagem" +
                                $"\n- /dano [nome] [dano]" +
                                $"\n\tReduz a vida atual do personagem" +
                                $"\n- /cura [nome] [cura]" +
                                $"\n\tAumenta a vida atual do personagem" +
                                $"\n- /lvlup [nome] [atributo]" +
                                $"\n\tAumenta o level de um personagem e o atributo selecionado em 1." +
                                $"\n\t`perto` ou `p` para Ataque de Perto" +
                                $"\n\t`longe` ou `l` para Ataque de Longe" +
                                $"\n\t`defesa` ou `d` para Defesa" +
                                $"\n\t`forca` ou `f` para Força" +
                                $"\n\t`vida` ou `v` para Vida" +
                                $"\n- /all" +
                                $"\n\tMostra todos os personagens e suas vidas" +
                                $"\n- /editar [nome] [novoNome] [level] [atkPerto] [atkLonge] [defesa] [forca] [vida]" +
                                $"\n\tUse para editar os valores de um personagem. Ou pessa ao peça ao **Guil** para editar manualmente(é mais rápido)" +
                                $"\n- /excluir [nome]" +
                                $"\n\tExclua um personagem" +
                                $"\n- /inventario [nome]" +
                                $"\n\tMostra todos os itens de um personagem" +
                                $"\n- /additem [nomePersonagem] [nomeItem] [quantidade]" +
                                $"\n\tAdiciona um item no inventário do personagem" +
                                $"\n- /removeitem [nomePersonagem] [nomeItem] [quantidade]" +
                                $"\n\tRemove um item do inventário do personagem" +
                                $"\n- /help" +
                                $"\n\tVocê sabe o que esse comando faz...");
        }
        
    }
}
