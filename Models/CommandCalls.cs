using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace marvin.Entities
{
    public static class CommandCalls
    {
        private static CommandService? commands;
        private static HttpRequestHandler? httpHandler;

        public static void Init(CommandService _commands, HttpRequestHandler _httpHandler)
        {
            commands = _commands;
            httpHandler = _httpHandler;
        }

        public static string GetCommandList()
        {
            List<CommandInfo> commandlist = commands.Commands.ToList();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("The following commands are available:");

            List<CommandInfo> listCommands = new List<CommandInfo>();
            List<CommandInfo> newsCommands = new List<CommandInfo>();
            List<CommandInfo> standAloneCommands = new List<CommandInfo>();

            foreach (CommandInfo c in commandlist)
            {
                switch (c.Module.Group)
                {
                    case "News":
                        newsCommands.Add(c);
                        break;
                    case "List":
                        listCommands.Add(c);
                        break;
                    case null:
                        standAloneCommands.Add(c);
                        break;
                    default:
                        standAloneCommands.Add(c);
                        break;
                }
            }

            sb.AppendLine("News commands:");
            foreach (CommandInfo c in newsCommands)
            {
                sb.AppendLine("News " + c.Name + "....." + c.Summary);
            }

            sb.AppendLine("List commands:");
            foreach (CommandInfo c in listCommands)
            {
                sb.AppendLine("List " + c.Name + "....." + c.Summary);
            }

            sb.AppendLine("Stand alone commands:");
            foreach (CommandInfo c in standAloneCommands)
            {
                sb.AppendLine(c.Name + "....." + c.Summary);
            }

            return sb.ToString();
        }
        public static List<Article> GetNewsLinks()
        {
            List<Article> result = httpHandler.GetArticles();

            return result;
        }
    }
}