using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace marvin.Entities
{
    public class BooleanTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            bool result;
            if (bool.TryParse(input, out result))
            {
                return Task.FromResult(TypeReaderResult.FromSuccess(result));
            }

            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Could not parse input as a boolean."));
        }
    }
}