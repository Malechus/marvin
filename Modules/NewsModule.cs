using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using marvin.Entities;
using NewsAPI;
using NewsAPI.Models;

namespace marvin.Modules
{
    [Group("News")]
    public class NewsModule : ModuleBase<SocketCommandContext>
    {
        [Command("top")]
        [Summary("Gets a sample of current news from the internet.")]
        public async Task GetNews()
        {
            await ReplyAsync("Really? How depressing.");
            List<Article> articles = CommandCalls.GetNewsLinks();
            foreach (Article a in articles)
            {
                await ReplyAsync(a.Title);
                await ReplyAsync(a.Description);
                await ReplyAsync(a.Url);
            }
            await ReplyAsync("There, now wasn't that a waste of time?");
        }
    }
}