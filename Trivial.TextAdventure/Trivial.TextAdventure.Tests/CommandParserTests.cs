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
using Trivial.TextAdventure.Parsing;

namespace Trivial.TextAdventure.Tests
{
    [TestFixture]
    public class CommandParserTests
    {
        [Test]
        public void ParseCommandFromCommandTokens()
        {
            var t_Parser = new CommandParser(_GetPatterns());

            var t_ParsedCommand = t_Parser.Parse(new List<CommandToken> { 
                new CommandToken(new Token("attack"), new CommandTokenType("ATTACK")), 
                new CommandToken(new Token("goblin"), new CommandTokenType("TARGET"))});

            t_ParsedCommand.HasValue.Should().BeTrue();
            t_ParsedCommand.Value.Type.Type.Should().Be("ATTACK");
            t_ParsedCommand.Value.Tokens[0].TextToken.Text.Should().Be("attack");
            t_ParsedCommand.Value.Tokens[0].Type.Type.Should().Be("ATTACK");
            t_ParsedCommand.Value.Tokens[1].TextToken.Text.Should().Be("goblin");
            t_ParsedCommand.Value.Tokens[1].Type.Type.Should().Be("TARGET");
        }

        private ImmutableList<CommandPattern> _GetPatterns() =>
            ImmutableList.Create(
                    new CommandPattern(new CommandType("ATTACK"),
                        ImmutableList.Create(
                            new CommandPatternToken(new CommandTokenType("ATTACK")),
                            new CommandPatternToken(new CommandTokenType("TARGET")))));
    }
}
