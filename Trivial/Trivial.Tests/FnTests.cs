using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Trivial.Functional.Functions;

namespace Trivial.Tests
{
    [TestFixture]
    public class FnTests
    {
        [Test]
        public void ConvertValueToFunction()
        {
            var t_Func = Fn(1);

            t_Func().Should().Be(1);
        }
    }
}
