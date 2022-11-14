using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Trivial.Helpful.Functions;

namespace Trivial.Tests
{
    [TestFixture]
    public class LetTests
    {
        [Test]
        public void TestMethod()
        {
            Let(1, 2, (a, b) =>
            {
                return Let(a, b, (a, b) =>
                {
                    return a + b;
                });
            });
        }

        [Test]
        public void Test()
        {
            var t_Result = GetSomething(false) switch
            {
                ResultError => 0,
                ResultValue<int> => 1,
                _ => -1
            };

            t_Result.Should().Be(1);
        }

        private interface Result { }
        private interface Result<T> : Result { }
        private class ResultError : Result<Exception> { }
        private class ResultValue<T> : Result<T> { }

        private Result GetSomething(bool Error)
        {
            return Error ? new ResultError() : new ResultValue<int>();
        }
    }
}
