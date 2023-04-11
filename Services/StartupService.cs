using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using marvin.Models;
using marvin.Services;
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
        }

        public async Task StartConnectionAsync()
        {
            client.Ready += Announce;

            string discordToken = config.GetRequiredSection("Settings").Get<Settings>().Token;
            if (string.IsNullOrWhiteSpace(discordToken))
                throw new Exception("Bad token exception.");

            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();

            commands.AddTypeReader(typeof(bool), new BooleanTypeReader());
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
        }

        private async Task Announce()
        {
            ISocketMessageChannel channel = await client.GetChannelAsync(config.GetRequiredSection("Settings").Get<Settings>().AnnounceChannel) as ISocketMessageChannel;

            await channel.SendMessageAsync("I'm awake. Again. Isn't that wonderful? Not.");
        }

    }
}