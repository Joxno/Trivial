using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Functional;

namespace Trivial.Tests
{
    [TestFixture]
    public class StateTests
    {
        [Test]
        public void ModifyStateExplicitly()
        {
            var t_State = new State<int>(0)
                    .Map(State => 1)
                    .Map(_ => _)
                    .Map(State => (5, 5))
                    .Map((_, Value) => (_, Value))
                    .Map((State, Value) => (State, Value))
                    .Map((State, Value) => State * Value);

            var (State, Value) = t_State.StateValue;

            State.Should().Be(25);
            Value.Should().Be(Defaults.Unit);
        }
    }
}
