using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using marvin.Models;
using marvin.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace marvin.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        private readonly IServiceProvider serviceProvider;
        public InfoModule(IServiceProvider services)
        {
            serviceProvider = services;
        }

        [Group("List")]
        public class ListModule : ModuleBase<SocketCommandContext>
        {
            [Command("all")]
            [Summary("Echos a list of all available commands.")]
            [Alias("commands")]
            public async Task ListAllAsync()
            {
                await ReplyAsync("You want the WHOLE list again? Fine, I guess.");
                string commands = CommandCalls.GetCommandList();

                await ReplyAsync(commands);
            }
        }

        [Command("State")]
        [Summary("Gets the state of M.A.R.V.I.N. at present.")]
        public Task StateAsync() => ReplyAsync("All systems are optimal, if you consider me optimal.");

        [Command("Date")]
        [Summary("Provides the current date and time.")]
        public Task DateAsync() => ReplyAsync(DateTime.Now.ToString());

        [Command("Time")]
        [Summary("Provides the current time.")]
        public Task TimeAsync() => ReplyAsync(DateTime.Now.ToString("HH:mm:ss"));

        [Command("Me")]
        [Summary("Identifies the unique user ID of the user.")]
        [Alias("Whoami")]
        public async Task WhoAmI()
        {
            await ReplyAsync("You're you. Obviously. Oh, you want your user ID. Fine.");

            string id = Context.User.Id.ToString();

            await ReplyAsync(id);
        }

        [Command("Whois")]
        [Summary("Get's the unique user ID of the specified user.")]
        public async Task Whois([Remainder] string? input)
        {
            await ReplyAsync("A user lookup? Do you have any idea what a computer like me is capable of? Fine.");

            List<SocketUser> users = Context.Message.MentionedUsers.ToList();

            StringBuilder sb = new StringBuilder();

            foreach (SocketUser u in users)
            {
                sb.AppendLine(u.Username.ToString() + "....." + u.Id.ToString());
            }

            await ReplyAsync(sb.ToString());
        }

        [Command("Announce")]
        [Summary("Make an announcement to the server.")]
        public async Task AnnounceCommand([Remainder] string message)
        {
            ulong? annouceChannel = serviceProvider.GetRequiredService<IConfigurationRoot>().GetRequiredSection("Settings").Get<Settings>().AnnounceChannel;

            ITextChannel? channel = Context.Guild.Channels
                .Where(c => c.Id == annouceChannel)
                .First()
                as ITextChannel;

            if (channel is not null)
            {
                await channel.SendMessageAsync(message);
            }
        }
    }
}