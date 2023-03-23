using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace marvin.Entities
{
    public static class CommandHandlers
    {
        private static CommandService? commands;

        public static void Init(CommandService _commands)
        {
            commands = _commands;
        }

        public static List<CommandInfo> GetCommandList()
        {
            List<CommandInfo> commandlist = commands.Commands.ToList();
            return commandlist;
        }
    }
}