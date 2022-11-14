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
    public class UnitTests
    {
        [Test]
        public void UnitComparison()
        {
            var t_Unit = new Unit();

            t_Unit.Should().Equals(new Unit());
        }
    }
}
