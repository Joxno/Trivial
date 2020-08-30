using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Trivial.Helpful.Func.FunctionZip;

namespace Trivial.Tests
{
    [TestFixture]
    public class ZipTests
    {
        [Test]
        public void ZipTwoEqualLengthCollections()
        {
            var t_First = new List<int> { 1, 2, 3 };
            var t_Second = new List<int> { 4, 5, 6};

            var t_Zipped = Zip(t_First, t_Second);

            t_Zipped.Should().HaveCount(3);
        }

        [Test]
        public void ZipTwoNonEqualLengthCollections()
        {
            var t_First = new List<int> { 1, 2, 3 };
            var t_Second = new List<int> { 4, 5, 6, 7, 8 };

            var t_Zipped = Zip(t_First, t_Second);

            t_Zipped.Should().HaveCount(3);
        }

        [Test]
        public void ZipWithFirstCollectionMoreThanSecondCollection()
        {
            var t_First = new List<int> { 1, 2, 3, 7, 8 };
            var t_Second = new List<int> { 4, 5, 6 };

            var t_Zipped = Zip(t_First, t_Second);

            t_Zipped.Should().HaveCount(3);
        }
    }
}
