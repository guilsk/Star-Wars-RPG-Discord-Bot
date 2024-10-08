﻿using Discord.Interactions;
using Discord.WebSocket;
using DiscordRPGBot.ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiscordRPGBot {
    internal class Program {
        static async Task Main(string[] args) {

            string jsonFilePath = "token.json";
            string discordToken = File.ReadAllText(jsonFilePath);

            if (string.IsNullOrEmpty(discordToken)) {
                Console.WriteLine("O token não foi configurado corretamente!");
                return;
            }

            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config => {
                    config.AddJsonFile(jsonFilePath, false);                    // Add the config file to IConfiguration variables
                })
                .ConfigureServices(services => {
                    services.AddSingleton<DiscordSocketClient>();               // Add the discord client to services
                    services.AddSingleton<InteractionService>();                // Add the interaction service to services
                    services.AddHostedService<InteractionHandlingService>();    // Add the slash command handler
                    services.AddHostedService<DiscordStartupService>();         // Add the discord startup service
                })
                .Build();

            await host.RunAsync();
        }
    }
}