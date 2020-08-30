using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.TextAdventure.Data;
using Trivial.TextAdventure.Interfaces;
using Trivial.TextAdventure.Parsing;

namespace Trivial.TextAdventure.Tests
{
    [TestFixture]
    public class TextParserTests
    {
        [Test]
        public void ParseSimpleAttackCommandFromText()
        {
            var t_Parser = new TextParser(_CreateLexer(), _CreateCommandTokenParser(), _CreateCommandParser());

            var t_ParsedCommand = t_Parser.Parse("attack goblin");

            t_ParsedCommand.HasValue.Should().BeTrue();
        }

        private ILexer _CreateLexer() => new Lexer();
        private ICommandTokenParser _CreateCommandTokenParser() => new CommandTokenParser(_GetCommandTokenPatterns());
        private ImmutableList<Pattern> _GetCommandTokenPatterns() =>
            ImmutableList.Create(
                new Pattern(ImmutableList.Create(
                    new PatternToken("(?i)(attack)", new CommandTokenType("ATTACK")),
                    new PatternToken("[a-zA-Z]+", new CommandTokenType("TARGET"))))
            );
        private ICommandParser _CreateCommandParser() => new CommandParser(_GetCommandPatterns());
        private ImmutableList<CommandPattern> _GetCommandPatterns() =>
            ImmutableList.Create(
                    new CommandPattern(new CommandType("ATTACK"),
                        ImmutableList.Create(
                            new CommandPatternToken(new CommandTokenType("ATTACK")),
                            new CommandPatternToken(new CommandTokenType("TARGET")))));
    }
}
