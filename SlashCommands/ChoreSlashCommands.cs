using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace marvin.SlashCommands
{
    public static class ChoreSlashCommands
    {
        public static List<SlashCommandBuilder> BuildChoreCommands()
        {
            List<SlashCommandBuilder> choreCommands = new List<SlashCommandBuilder>();

            choreCommands.Add(BuildGetChores());

            return choreCommands;
        }

        private static SlashCommandBuilder BuildGetChores()
        {
            SlashCommandBuilder getChoreBuilder = new SlashCommandBuilder()
                .WithName("get-chores")
                .WithDescription("Gets a list of chores.")
                .AddOption("day", ApplicationCommandOptionType.String, "The day for which you want a list of chores.");

            return getChoreBuilder;
        }

        public static async Task HandleGetChores()
        {

        }
    }
}