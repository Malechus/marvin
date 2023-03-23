using System;
using System.Collections.Generic;
using System.Linq;
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

namespace marvin.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Group("List")]
        public class ListModule : ModuleBase<SocketCommandContext>
        {
            [Command("all")]
            [Summary("Echos a list of all available commands.")]
            public async Task ListAllAsync()
            {
                await ReplyAsync("You want the WHOLE list again? Fine, I guess.");
                List<CommandInfo> commands = CommandHandlers.GetCommandList();
                foreach (CommandInfo c in commands)
                {
                    await ReplyAsync(c.Name + "....." + c.Summary);
                }
            }
        }

        [Command("State")]
        [Summary("Gets the state of M.A.R.V.I.N. at present.")]
        public Task StateAsync() => ReplyAsync("All systems are optimal, if you consider me optimal.");
    }
}