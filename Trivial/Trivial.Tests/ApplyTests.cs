using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;

namespace Trivial.Tests
{
    [TestFixture]
    public class ApplyTests
    {
        [Test]
        public void ApplySingleParameterFunction()
        {
            var t_Applied = _GetSingleParameterFunction().Apply(5);

            t_Applied().Should().Be(5);
        }

        private Func<int, int> _GetSingleParameterFunction() => (i) => i;
    }
}
