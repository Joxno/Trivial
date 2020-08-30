using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.TextAdventure.Data;
using Trivial.TextAdventure.Loaders;

namespace Trivial.TextAdventure.Tests
{
    [TestFixture]
    public class CommandPatternLoaderTests
    {
        [Test]
        public void LoadPatternsFromJSON()
        {
            var t_Loader = new CommandPatternLoader();

            var t_Patterns = t_Loader.LoadFromJson(_GetJSON());

            t_Patterns.HasValue.Should().BeTrue();
            t_Patterns.Value.Item1.Should().HaveCountGreaterThan(0);
            t_Patterns.Value.Item2.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public void LoadPatternsFromInvalidJSON()
        {
            var t_Loader = new CommandPatternLoader();
            var t_Patterns = t_Loader.LoadFromJson(new JSON(""));

            t_Patterns.HasValue.Should().BeFalse();
        }

        private JSON _GetJSON() => 
            new JSON(Properties.Resources.CommandPattern_json);
    }
}
