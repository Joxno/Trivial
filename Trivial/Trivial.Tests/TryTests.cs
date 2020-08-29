using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using Trivial.Helpful;
using Trivial.Utilities;

namespace Trivial.Tests
{
    public class TryTests
    {

        [Test]
        public void TryFrom()
        {
            var t_Maybe = Try.From(() => 0);

            t_Maybe.HasValue.Should().BeTrue();
            t_Maybe.Value.Should().Be(0);
        }

        [Test]
        public void TryFromFailed()
        {
            var t_Maybe = Try.From<int>(() => throw new Exception());

            t_Maybe.HasValue.Should().BeFalse();
        }

        [Test]
        public void TryGetFrom()
        {
            var t_Maybe = Try.GetFrom((int i) => i);

            t_Maybe(5).Value.Should().Be(5);
            t_Maybe(5).Then(V => V.Should().Be(5));
        }

        [Test]
        public void TryGetInvokeFrom()
        {
            var t_Identity = Try.GetInvokeFrom((int i) => i);

            t_Identity(5).HasError.Should().BeFalse();
            t_Identity(5).Value.Value.Should().Be(5);
        }

        [Test]
        public void TryGetInvokeFromFailed()
        {
            var t_ExceptionFunc = Try.GetInvokeFrom((Func<int, int>)((int i) => throw new Exception()));
            Action t_NotThrow = () => t_ExceptionFunc(5);


            t_NotThrow.Should().NotThrow();
            t_ExceptionFunc(5).HasError.Should().BeTrue();
        }

        [Test]
        public void TryGetInvokeFromNoValue()
        {
            var t_Maybe = Try.GetInvokeFrom((int i) => _GetDummyByIdFailed(i))(5);
            
            t_Maybe.HasError.Should().BeFalse();
            t_Maybe.Value.HasValue.Should().BeFalse();
        }

        [Test]
        public void TryInvoke()
        {
            var t_Test = 0;
            Action t_Func = () => { t_Test = 10; };

            var t_Result = Try.Invoke(t_Func);

            t_Result.HasError.Should().BeFalse();
            t_Test.Should().Be(10);
        }

        [Test]
        public void TryInvokeFailed()
        {
            Action t_Func = () => { throw new Exception(); };

            var t_Result = Try.Invoke(t_Func);

            t_Result.HasError.Should().BeTrue();
        }

        private class Dummy { }

        private Dummy _CreateDummy() => new Dummy();
        private Dummy _CreateDummyFailed() => null;
        private Dummy _GetDummyById(int Id) => new Dummy();
        private Dummy _GetDummyByIdFailed(int Id) => null;

        private int _GetIdentity(int Id) => Id;

        private Dummy _FakeCopyDummy(Dummy Dummy) => new Dummy();

        private delegate TR OneParamDel<in T, out TR>(T Arg);
        private Func<T, Maybe<T>> ToFunc<T>(Func<T, Maybe<T>> F) =>
            (P) => F(P);
    }
}