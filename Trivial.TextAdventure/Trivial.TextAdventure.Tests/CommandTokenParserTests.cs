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
    public class CommandTokenParserTests
    {
        [Test]
        public void ParseAttackTargetPattern()
        {
            var t_Parser = new CommandTokenParser(_GetPatterns());

            var t_ParsedString = t_Parser.Parse(new List<Token> { new Token("attack"), new Token("goblin") });

            t_ParsedString.HasValue.Should().BeTrue();
            t_ParsedString.Value.ElementAt(0).Type.Type.Should().Be("ACTION");
            t_ParsedString.Value.ElementAt(1).Type.Type.Should().Be("TARGET");
        }

        [Test]
        public void ParseInvalidPattern()
        {
            var t_Parser = new CommandTokenParser(_GetPatterns());
            var t_ParsedString = t_Parser.Parse(new List<Token> { new Token("wander"), new Token("aimlessly") });

            t_ParsedString.HasValue.Should().BeFalse();
        }

        private ImmutableList<Pattern> _GetPatterns() =>
            ImmutableList.Create(
                new Pattern(ImmutableList.Create(
                    new PatternToken("(?i)(attack)", new CommandTokenType("ACTION")), 
                    new PatternToken("[a-zA-Z]+", new CommandTokenType("TARGET"))))
            );
    }
}
