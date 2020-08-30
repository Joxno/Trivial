using FluentAssertions;
using NUnit.Framework;
using Trivial.TextAdventure.Parsing;

namespace Trivial.TextAdventure.Tests
{
    public class LexerTests
    {
        [Test]
        public void TokenizeSmallString()
        {
            var t_Lexer = new Lexer();

            var t_Tokens = t_Lexer.Tokenize("One Small Cat");

            t_Tokens.Should().HaveCount(3);
        }

        [Test]
        public void TokenizeEmptyString()
        {
            var t_Lexer = new Lexer();

            var t_Tokens = t_Lexer.Tokenize("");

            t_Tokens.Should().HaveCount(0);
        }
    }
}