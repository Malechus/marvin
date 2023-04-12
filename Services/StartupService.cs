using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using marvin.Models;
using marvin.Services;
using marvin.SlashCommands;
using marvin.TypeReaders;
using Microsoft.Extensions.Configuration;

namespace marvin.Services
{
    public class StartupService
    {
        private readonly IServiceProvider provider;
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly IConfigurationRoot config;
        private readonly Settings settings;

        public StartupService(
            IServiceProvider _serviceProvider,
            DiscordSocketClient _client,
            CommandService _commands,
            IConfigurationRoot _config)
        {
            provider = _serviceProvider;
            client = _client;
            commands = _commands;
            config = _config;
            settings = config.GetRequiredSection("Settings").Get<Settings>();
        }

        public async Task StartConnectionAsync()
        {
            client.Ready += Announce;
            client.Ready += BuildSlashCommandsAsync;

            string discordToken = settings.Token;
            if (string.IsNullOrWhiteSpace(discordToken))
                throw new Exception("Bad token exception.");

            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();

            commands.AddTypeReader(typeof(bool), new BooleanTypeReader());
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
        }

        private async Task Announce()
        {
            ISocketMessageChannel channel = await client.GetChannelAsync(settings.AnnounceChannel) as ISocketMessageChannel;

            await channel.SendMessageAsync("I'm awake. Again. Isn't that wonderful? Not.");
        }

        private async Task BuildSlashCommandsAsync()
        {
            List<SlashCommandBuilder> choreCommands = ChoreSlashCommands.BuildChoreCommands();

            foreach (SlashCommandBuilder sb in choreCommands)
            {
                try
                {
                    await client.Rest.CreateGuildCommand(sb.Build(), settings.ServerID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(sb.Name, ex.Message);
                }
            }
        }

    }
}