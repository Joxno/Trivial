using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;
using Trivial.TextAdventure.Data;
using Trivial.TextAdventure.Interfaces;
using static Trivial.Helpful.Func.FunctionZip;

namespace Trivial.TextAdventure.Parsing
{
    public class CommandParser : ICommandParser
    {
        private ImmutableList<CommandPattern> m_Patterns;
        public CommandParser(ImmutableList<CommandPattern> Patterns) => 
            m_Patterns = Patterns;

        public Func<IEnumerable<CommandToken>, Maybe<Command>> Parse =>
            (Tokens) =>
                m_Patterns
                    .Select(_ParsePattern.Apply(Tokens))
                    .Where(M => M.HasValue)
                    .OrderByDescending(M => M.Value.Tokens.Count)
                    .FirstOrDefault();

        private Func<IEnumerable<CommandToken>, CommandPattern, Maybe<Command>> _ParsePattern =>
            (Tokens, Pattern) =>
                Zip(Tokens, Pattern.Pattern)
                    .Select(TP => _Predicate(TP.First, TP.Second))
                    .All(B => B)
                        ? new Command(Pattern.Type, Tokens.ToImmutableList())
                        : null;

        private Func<CommandToken, CommandPatternToken, bool> _Predicate =>
            (T, P) =>
                T.Type == P.Type;
                
    }
}
