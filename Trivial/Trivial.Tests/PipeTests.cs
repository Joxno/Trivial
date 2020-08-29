using FluentAssertions;
using NUnit.Framework;
using System;
using Trivial.Helpful;

namespace Trivial.Tests
{
    [TestFixture]
    public class PipeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PipeTwoUselessFunctions()
        {
            Func<int, float> t_F1 = (A) => A;
            Func<float, int> t_F2 = (A) => (int)A;
            var t_Composed = Function.Pipe(t_F1, t_F2);

            t_Composed(5).Should().Be(5);
        }

        [Test]
        public void PipeTwoChangingFunctions()
        {
            Func<int, float> t_Add5 = (A) => A + 5;
            Func<float, int> t_Times2 = (A) => (int)A * 2;

            var t_Add5Times2 = Function.Pipe(t_Add5, t_Times2);

            t_Add5Times2(5).Should().Be(20);
        }

        [Test]
        public void PipeTwoFunctionsWithDifferentTypes()
        {
            Func<int, float> t_ConvertToFloat = A => A;
            Func<float, float> t_Add1AndAHalf = A => A + 1.5f;

            var t_ConvertAndAdd = Function.Pipe(t_ConvertToFloat, t_Add1AndAHalf);

            t_ConvertAndAdd(5).Should().Be(6.5f);
        }

        [Test]
        public void PipeToExtension()
        {
            Func<int, int> t_Double = A => A * 2;

            var t_Double4Times =
                t_Double
                    .PipeTo(t_Double)
                    .PipeTo(t_Double)
                    .PipeTo(t_Double);

            t_Double4Times(2).Should().Be(32);
        }
    }
}