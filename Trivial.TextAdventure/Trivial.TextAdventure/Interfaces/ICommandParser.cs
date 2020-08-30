using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;
using Trivial.TextAdventure.Data;

namespace Trivial.TextAdventure.Interfaces
{
    public interface ICommandParser
    {
        public Func<IEnumerable<CommandToken>, Maybe<Command>> Parse { get; }
    }
}
