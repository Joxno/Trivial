using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Functional;
using Trivial.Utilities;

namespace Trivial.Tests
{
    [TestFixture]
    public class CurryTests
    {
        [Test]
        public void TwoParameterCurry()
        {
            Func<int, int, int> t_Div = (x, y) => x / y;

            var t_Curried = t_Div.Curry();

            t_Curried(10)(5).Should().Be(2);
        }

        [Test]
        public void ThreeParameterCurry()
        {
            Func<int, int, int, int> t_Times = (x, y, z) => x * y * z;

            var t_Curried = t_Times.Curry();

            t_Curried(2)(2)(3).Should().Be(12);
        }

        [Test]
        public void FourParameterCurry()
        {
            Func<int, int, int, int, int> t_Add = (x, y, z, j) => x + y + z + j;

            var t_Curried = t_Add.Curry();

            t_Curried(1)(2)(3)(4).Should().Be(10);
        }

        [Test]
        public void FiveParameterCurry()
        {
            Func<int, int, int, int, int, int> t_Add = (x, y, z, j, k) => x + y + z + j + k;

            var t_Curried = t_Add.Curry();

            t_Curried(1)(2)(3)(4)(5).Should().Be(15);
        }

        [Test]
        public void SixParameterCurry()
        {
            Func<int, int, int, int, int, int, int> t_Add =
                (x, y, z, j, k, l)
                => x + y + z + j + k + l;

            var t_Curried = t_Add.Curry();

            t_Curried(1)(2)(3)(4)(5)(6).Should().Be(21);
        }

        [Test]
        public void CurriedFunctionCall()
        {
            Func<int, string, int, float, int> t_RandomFunc = 
                (a, b, c, d) => (int)(a + int.Parse(b) + c + d);

            var t_Curried = t_RandomFunc.Curry();
            var t_Result =
                t_Curried.ToMaybe()
                .Map(F => F.Apply(2))
                .Map(F => F.Apply("12"))
                .Map(F => F.Apply(2))
                .Map(F => F.Apply(2.2f))
                .Bind(Try.From);

            t_Result.HasValue.Should().BeTrue();
            t_Result.Value.Should().Be(18);
        }

    }
}
