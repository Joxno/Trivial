using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.TextAdventure.Data
{
    public record Command(CommandType Type, ImmutableList<CommandToken> Tokens);
}
