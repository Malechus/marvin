using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace marvin.Services
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly IServiceProvider serviceProvider;

        public CommandHandler(
            DiscordSocketClient _client,
            CommandService _commands,
            IServiceProvider _provider
        )
        {
            client = _client;
            commands = _commands;
            serviceProvider = _provider;

            client.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            var message = s as SocketUserMessage;
            if (message == null) return;
            if (message.Author.Id == client.CurrentUser.Id) return;

            var context = new SocketCommandContext(client, message);

            int argPos = 0;
            if (message.HasCharPrefix('$', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                var result = await commands.ExecuteAsync(context, argPos, serviceProvider);

                if (!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync("I don't know that command. Sorry. I guess you got that one wrong.");
                }
            }
        }


    }
}