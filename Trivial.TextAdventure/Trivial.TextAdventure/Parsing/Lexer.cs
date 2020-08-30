using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.TextAdventure.Data;
using Trivial.TextAdventure.Interfaces;

namespace Trivial.TextAdventure.Parsing
{
    public class Lexer : ILexer
    {
        public Func<string, IEnumerable<Token>> Tokenize => 
            (string Text) => 
                Text.Split(" ")
                    .AsEnumerable()
                    .Where(S => !string.IsNullOrEmpty(S))
                    .Select(S => new Token(S));
    }
}
