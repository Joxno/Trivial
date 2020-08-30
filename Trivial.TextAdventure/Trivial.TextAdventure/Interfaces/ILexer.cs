using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.TextAdventure.Data;

namespace Trivial.TextAdventure.Interfaces
{
    public interface ILexer
    {
        public Func<string, IEnumerable<Token>> Tokenize { get; }
    }
}
