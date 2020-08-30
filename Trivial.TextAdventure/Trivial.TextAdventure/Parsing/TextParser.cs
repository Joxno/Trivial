using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;
using Trivial.TextAdventure.Data;
using Trivial.TextAdventure.Interfaces;
using static Trivial.Helpful.Function;

namespace Trivial.TextAdventure.Parsing
{
    public class TextParser : ITextParser
    {
        private ILexer m_Lexer;
        private ICommandTokenParser m_CommandTokenParser;
        private ICommandParser m_CommandParser;

        public TextParser(ILexer Lexer, ICommandTokenParser TokenParser, ICommandParser CommandParser) =>
            (m_Lexer, m_CommandTokenParser, m_CommandParser) = (Lexer, TokenParser, CommandParser);

        public Func<string, Maybe<Command>> Parse =>
            (Text) =>
            Pipe(
                    m_Lexer.Tokenize,
                    m_CommandTokenParser.Parse
            )(Text)
            .Bind(m_CommandParser.Parse);
    }
}
