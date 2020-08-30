using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;
using Trivial.TextAdventure.Data;

namespace Trivial.TextAdventure.Interfaces
{
    public interface ICommandTokenParser
    {
        public Func<IEnumerable<Token>, Maybe<IEnumerable<CommandToken>>> Parse { get; }
    }
}
