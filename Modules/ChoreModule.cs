using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using marvin.Models;
using marvin.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace marvin.Modules
{
    public class ChoreModule : ModuleBase<SocketCommandContext>
    {
        private readonly IServiceProvider serviceProvider;

        public ChoreModule(IServiceProvider services)
        {
            serviceProvider = services;
        }

        [Command("Chores")]
        [Summary("Gets the daily and weekly chores for a given day, or all of them.")]
        public async Task GetChoresAsync([Remainder] string day = null)
        {
            DbService dbService = serviceProvider.GetRequiredService<DbService>();

            string response;

            if (day is not null)
            {
                try
                {
                    response = dbService.GetChoresbyDay(day);
                }
                catch (ArgumentException ex)
                {
                    await ReplyAsync("That is not a valid query.");
                    return;
                }
            }
            else
            {
                response = dbService.GetAllChores();
            }

            //If response exceeds macimum length, split into lines and feed to server one at a time.
            if (response.Length <= 2000)
            {
                await ReplyAsync(response);
            }
            else
            {
                string[] responses = response.Split(
                    new string[] { Environment.NewLine },
                    StringSplitOptions.RemoveEmptyEntries
                );

                foreach (string s in responses)
                {
                    await ReplyAsync(s);
                }
            }
        }
    }
}