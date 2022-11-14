using NUnit.Framework;
using System;
using Trivial.Helpful;
using FluentAssertions;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using Trivial.Utilities;

namespace Trivial.Tests
{
    public class MaybeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateMaybeFromNull()
        {
            Maybe<int> t_Maybe = null;

            t_Maybe.Should().NotBeNull();
            t_Maybe.HasValue.Should().BeFalse();
        }

        [Test]
        public void CreateMaybeConcreteFromNull()
        {
            Func<Maybe<DummyImpl>> t_CreateMaybe = () => new Maybe<DummyImpl>();
            //Maybe<DummyImpl> t_Maybe = null;
            Maybe<int> t_be = null;
            //Maybe<IDummy> t_May = null;
            //Maybe<int> t_Ma = new TypeWrapper<int>();
            Maybe<int> t_MI = 0;

            Maybe<IDummy> t_InterfaceDummy = _CreateOrFailDummyExpr(0);
        }

        [Test]
        public void CreateMaybeWithValue()
        {
            var t_Maybe = new Maybe<int>(5);

            t_Maybe.HasValue.Should().BeTrue();
            t_Maybe.Value.Should().Be(5);
        }

        [Test]
        public void CreateMaybeWithoutValue()
        {
            var t_Maybe = new Maybe<int>();
            Func<int> t_Thrower = () => t_Maybe.Value;

            t_Maybe.HasValue.Should().BeFalse();
            t_Thrower.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void CreateMaybeWithInterfaceNoValue()
        {
            var t_Maybe = new Maybe<IDummy>();
            Func<IDummy> t_Thrower = () => t_Maybe.Value;

            t_Maybe.HasValue.Should().BeFalse();
            t_Thrower.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void CreateMaybeWithConcrete()
        {
            var t_Maybe = new Maybe<IDummy>(new DummyImpl());

            t_Maybe.HasValue.Should().BeTrue();
            t_Maybe.Value.Should().NotBeNull();
        }

        [Test]
        public void ImplicitReturnMaybeOfInterfaceFromConcrete()
        {
            Func<Maybe<IDummy>> t_CreateMaybe = () => new DummyImpl();

            t_CreateMaybe.Should().NotThrow();

            var t_Maybe = t_CreateMaybe();
            t_Maybe.HasValue.Should().BeTrue();
            t_Maybe.Value.Should().NotBeNull();
        }

        [Test]
        public void MaybeBind()
        {
            Maybe<int> M2 = null;
            Maybe<int> M3 = new Maybe<int>();
            Func<int, Maybe<int>> F1 = I => I;
            Func<int, Maybe<int>> F2 = I => I;

            //Func<int, Maybe<int>> M = I => Maybe<int>.Bind(F1(I), I2 => F2(I2));

            var t_Maybe =
                new Maybe<int>(5)
                .Bind(_IntToMaybeInt)
                .Bind(F2)
                .Bind(F1)
                .Bind(I => (Maybe<int>)null);

            t_Maybe.HasValue.Should().BeFalse();
        }

        [Test]
        public void MaybeLinqSyntax()
        {
            var t_ma = new Maybe<int>(3);
            var t_mb = new Maybe<int>(6);
            var t_Result =
                    from a in t_ma
                    from b in t_mb
                    select a > b ? a : b;

            t_Result.HasValue.Should().BeTrue();
            t_Result.Value.Should().Be(6);
        }

        [Test]
        public void MaybeMap()
        {
            Func<int, int> t_MultiplyBy2 = i => i * 2;
            var t_Maybe =
                new Maybe<int>(10)
                .Map(t_MultiplyBy2);

            t_Maybe.HasValue.Should().BeTrue();
            t_Maybe.Value.Should().Be(20);
        }

        [Test]
        public void IEnumerableMaybeMap()
        {
            Func<int, int> t_MultiplyBy2 = i => i * 2;
            Action<int> t_PrintNumber = i => Console.WriteLine(i);
            Func<int, int> t_PrintIdComposed = Function.Pipe(t_PrintNumber, Functions.Identity<int>());
            Func<int, Maybe<int>> t_FilterOver3 = i => i > 3 ? null : new Maybe<int>(i);

            var t_List = 
                new List<Maybe<int>> { 1, 2, 3, 4, null, 7, null, null, 9, 12, null, 15 }
                .Select(M => M
                    .Bind(t_FilterOver3)
                    .Map(t_MultiplyBy2)
                    .Map(t_PrintIdComposed))
                .Where(M => M.HasValue)
                .Select(M => M.Value)
                .ToList();

            t_List.Should().HaveCount(3);
            t_List[0].Should().Be(2);
            t_List[1].Should().Be(4);
            t_List[2].Should().Be(6);
        }

        [Test]
        public void MaybeWithSwitchExpression()
        {
            Maybe<int> t_Maybe = null;

            bool t_isNull = t_Maybe.HasValue switch {
                true => false,
                false => true,
            };

            t_isNull.Should().BeTrue();
        }

        private Maybe<int> _IntToMaybeInt(int i) => i;

        private Maybe<IDummy> _CreateOrFailDummyExpr(int i) =>
            i > 0 ? new DummyImpl() : null;

        private Maybe<IDummy> _CreateOrFailDummyStatement(int i)
        {
            if (i > 0)
                return new DummyImpl();

            return (DummyImpl)null;
        }

        private Maybe<IDummy> _CreateOrFailDummyStatementWithMaybeNull(int i)
        {
            if (i > 0)
                return new DummyImpl();

            return Maybe.Null;
        }

        private Maybe<IDummy> _CreateOrFailDummyCoalesceExpr() =>
            null ?? new DummyImpl();

        private Maybe<IDummy> _CreateOrFailDummyVariableReturn()
        {
            DummyImpl t_Dummy = null;

            return t_Dummy;
        }

        private interface IDummy
        {

        }

        private class DummyImpl : IDummy
        {

        }

        private class DummyData
        {
            public int Data { get; set; } = 0;
        }

    }
}