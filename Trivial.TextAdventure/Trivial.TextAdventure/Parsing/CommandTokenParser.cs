using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using Trivial.Helpful;
using Trivial.TextAdventure.Data;
using Trivial.TextAdventure.Interfaces;
using Trivial.Utilities;
using static Trivial.Helpful.Func.FunctionZip;
using static Trivial.Helpful.Func.FunctionMaybe;

namespace Trivial.TextAdventure.Parsing
{
    public class CommandTokenParser : ICommandTokenParser
    {
        private ImmutableList<Pattern> m_Patterns;

        public CommandTokenParser(ImmutableList<Pattern> Patterns) => 
            m_Patterns = Patterns;

        public Func<IEnumerable<Token>, Maybe<IEnumerable<CommandToken>>> Parse =>
            (Tokens) =>
                m_Patterns
                    .Select(_ParsePattern.Apply(Tokens))
                    .Where(M => M.HasValue)
                    .OrderByDescending(M => M.Value.Count())
                    .FirstOrDefault();

        private Func<IEnumerable<Token>, Pattern, Maybe<IEnumerable<CommandToken>>> _ParsePattern =>
            (Tokens, Pattern) =>
                Zip(Tokens, Pattern.Tokens)
                    .Select((TP) => _RegexPattern(TP.First, TP.Second))
                    .Raise();

        private Func<Token, PatternToken, Maybe<CommandToken>> _RegexPattern =>
            (Token, PatternToken) =>
                Regex.IsMatch(Token.Text, PatternToken.Regex)
                    ? new CommandToken(Token, PatternToken.Type)
                    : null;
    }
}
