using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using Trivial.Helpful;
using Trivial.TextAdventure.Data;
using Trivial.TextAdventure.DTO;
using Trivial.TextAdventure.Interfaces;
using Trivial.Utilities;
using static Trivial.Helpful.Def;

namespace Trivial.TextAdventure.Loaders
{
    public class CommandPatternLoader : ICommandPatternLoader
    {
        public Func<JSON, Maybe<(ImmutableList<CommandPattern>, ImmutableList<Pattern>)>> LoadFromJson =>
            (Json) =>
                Try.From(_Deserialize.Apply(Json))
                .Bind(_Extract);
            
        private Func<JSON, CommandPatternsDTO> _Deserialize =>
            (Json) =>
                JsonSerializer.Deserialize<CommandPatternsDTO>(Json.Text);

        private Func<CommandPatternsDTO, Maybe<(ImmutableList<CommandPattern>, ImmutableList<Pattern>)>> _Extract =>
            (Patterns) => 
                Let(_ExtractCommandPattern(Patterns.CommandPatterns), 
                    _ExtractPatterns(Patterns.CommandPatterns), 
                    (CP, P) => 
                        CP.HasValue && P.HasValue 
                        ? (CP.Value, P.Value).ToMaybe() 
                        : null
                )();

        private Func<CommandPatternDTO[], Maybe<ImmutableList<CommandPattern>>> _ExtractCommandPattern =>
            (Patterns) =>
                Patterns
                    .Select(P =>
                        Let(new CommandType(P.Command),
                            _ExtractPattern(P.Pattern)
                                .Select(L => L
                                    .Select(_ConvertToCommandPatternToken)
                                        .ToImmutableList()),
                            (T, P) => 
                                P.HasValue ? new CommandPattern(T, P.Value) : null
                            )()
                        .ToMaybe()
                    )
                    .ToImmutableList()
                    .Raise();

        private Func<CommandPatternDTO[], Maybe<ImmutableList<Pattern>>> _ExtractPatterns =>
            (Patterns) =>
                Patterns
                    .Select(P => 
                        _ExtractPattern(P.Pattern)
                            .Select(P => new Pattern(P)))
                    .ToImmutableList()
                    .Raise();

        private Func<PatternDTO[], Maybe<ImmutableList<PatternToken>>> _ExtractPattern =>
            (Pattern) =>
                new Maybe<PatternDTO[]>(Pattern.Count() > 0 ? Pattern : null)
                    .Select(P =>
                        P.Select(P => new PatternToken(P.Regex, new CommandTokenType(P.Type)))
                        .ToImmutableList());
        private Func<PatternToken, CommandPatternToken> _ConvertToCommandPatternToken =>
            (Token) =>
                new CommandPatternToken(Token.Type);
    }
}
