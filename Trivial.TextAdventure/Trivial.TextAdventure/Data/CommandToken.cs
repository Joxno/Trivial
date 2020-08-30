using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.TextAdventure.Data
{
    public record CommandToken(Token TextToken, CommandTokenType Type);
}
