using NUnit.Framework;
using System;
using Trivial.Helpful;
using Trivial.Utilities;
using FluentAssertions;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Linq;

namespace Trivial.Tests
{
    public class MaybeExtensionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DoSomeStuff()
        {
            Func<int, int> t_Add5 = X => X + 5;
            Func<int, int> t_Times2 = X => X * 2;
            Func<int, Maybe<int>> t_DoSomeStuff = (X) => { if (X % 2 == 0) return X; return new Maybe<int>(); };
            var t_Set = Enumerable.Range(0, 10);

            t_Set
                .Select(M => t_DoSomeStuff(M)
                            .Then(t_Add5)
                            .Then(t_Times2))
                .Where(M => M.HasValue)
                .ToList();
        }

    }
}