using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;

namespace Trivial.Tests
{
    [TestFixture]
    public class EffectTests
    {
        [Test]
        public void SimpleEffect()
        {

        }

        private class DummyEffect : IEffect
        {
            public Action Effect { get; init; }
        }

        private class WriteFile : IEffect<Unit>
        {
            public WriteFile(string Path, string Content) =>
                Effect = () => { File.WriteAllText(Path, Content); return Defaults.Unit; };
            public Func<Unit> Effect { get; init; }

            public static implicit operator Effect<WriteFile, Unit>(WriteFile F) =>
                Trivial.Helpful.Effect.Create<WriteFile, Unit>(F);
        }

        private Effect<WriteFile, Unit> SaveName(string Name) =>
            new WriteFile("test.json", "Testing");
            
    }
}
