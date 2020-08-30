using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;
using Trivial.TextAdventure.Data;

namespace Trivial.TextAdventure.Interfaces
{
    public interface ITextParser
    {
        public Func<string, Maybe<Command>> Parse { get; }
    }
}
