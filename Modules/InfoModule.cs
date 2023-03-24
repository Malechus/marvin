using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using marvin.Entities;
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
    }
}